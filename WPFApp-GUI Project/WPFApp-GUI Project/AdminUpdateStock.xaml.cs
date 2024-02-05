using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for AdminUpdateStock.xaml
    /// </summary>
    public partial class AdminUpdateStock : UserControl
    {
        CreateDBconnection con = new CreateDBconnection();
        
        private static string itemID = "";
        int availQty = 0;
        int enterQty = 0;
        private static int idHitCount = 0;

        public AdminUpdateStock()
        {
            InitializeComponent();
        }
               

        private void validateIemID(string item)
        {
            try
            {
                // Check if the enterd Item ID is valid
                string sqlReadItem = $"SELECT COUNT(*) FROM Items WHERE Item_ID = '{item}'";

                SqlCommand cmdReadItem = new SqlCommand(sqlReadItem, con.GetDBconnetion());
                idHitCount = (int)cmdReadItem.ExecuteScalar();

                if (idHitCount == 1)
                {
                    itemID = item;
                    borderAadminAddItem.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemID}.jpg", UriKind.Absolute)));

                    // Get the currently available qty of the Item
                    string sqlReadQty = $"SELECT Item_Qty FROM Items WHERE Item_Id = '{itemID}'";
                    SqlCommand cmdReadQty = new SqlCommand(sqlReadQty, con.GetDBconnetion());
                    SqlDataReader reader = cmdReadQty.ExecuteReader();
                   
                    if (reader.Read())
                    {
                        availQty = Convert.ToInt32(reader["Item_Qty"]);
                    }
                    labelQty.Content = availQty;
                }
                else
                {
                    labelQty.Foreground = Brushes.Red;
                    labelQty.Content = "Invalid Item ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        //Button Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxItemID.Text) && textBoxItemID.Text.Length == 4)
            {
                // Again validate th input Qty
                validateIemID(textBoxItemID.Text);

                if (idHitCount == 1)
                {
                    if (!String.IsNullOrWhiteSpace(textBoxQty.Text) && Convert.ToInt32(textBoxQty.Text) > 0 && textBoxQty.Text.All(char.IsDigit))
                    {
                        try
                        {
                            enterQty = Convert.ToInt32(textBoxQty.Text);
                            int totalQty = availQty + enterQty;
                            //  Update new Qty in database
                            string sqlUpdateQty = $"UPDATE Items SET Item_Qty = '{totalQty}' WHERE Item_ID = '{itemID}'";
                            SqlCommand cmdUpdateQty = new SqlCommand(sqlUpdateQty, con.GetDBconnetion());
                            cmdUpdateQty.ExecuteNonQuery();

                            MsgWindow msgWindow = new MsgWindow();
                            msgWindow.textBoxMsg.Foreground = Brushes.Blue;
                            msgWindow.textBoxMsg.Text = "Update Success !";
                            msgWindow.Show();

                            labelQty.Content = totalQty;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occur while update database\n\n" + ex.Message);
                        }
                    }
                    else
                    {
                        labelQty.Foreground = Brushes.Red;
                        labelQty.Content = "Invalid Qty !";
                    }
                }
            }
            else
            {
                labelQty.Foreground = Brushes.Red;
                labelQty.Content = "Invalid Item ID";
            }
        }


        // Text Changes
        private void textBoxItemID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxItemID.Text) && textBoxItemID.Text.Length == 4)
            {
                labelQty.Foreground = Brushes.Black;
                validateIemID(textBoxItemID.Text);
            }

            else
            {
                if (textBoxItemID.Text.Length == 4)
                {
                    labelQty.Foreground = Brushes.Red;
                    labelQty.Content = "Invalid Item ID";
                }
                else
                {
                    labelQty.Foreground = Brushes.Black;
                    labelQty.Content = "";
                }
                borderAadminAddItem.Background = null;
            }
        }


        //Button Enter and Leave
        private void ButtonConfirm_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(1.5);
            }
        }

        private void ButtonConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 95, 237, 203));
                button.BorderThickness = new Thickness(1.8);
            }
        }

        
    }
}
