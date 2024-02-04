using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for CartQty.xaml
    /// </summary>
    public partial class CartQty : Window
    {
        CreateDBconnection con = new CreateDBconnection();
        private string cusID;
        private string itemID;
        private static int itemQtyLeft;
        private static int buyQty = 0;

        private int itemPrice;
        private int total;

        private bool qtyReady = false;
        private bool addSuccess = false;
        public CartQty(string itemID)
        {
            InitializeComponent();
            this.itemID = itemID;
            cusID = ShoppingWindow.getLoggedID();

            setItem();
        }

        public void setItem()
        {
            try
            {
                //Reading Item information from database
                string sqlReadItem = $"SELECT * FROM Items WHERE Item_ID = '{itemID}'";
                SqlCommand cmdReadItem = new SqlCommand(sqlReadItem, con.GetDBconnetion());
                SqlDataReader reader = cmdReadItem.ExecuteReader();

                if (reader.Read())
                {
                    itemQtyLeft = Convert.ToInt32(reader["Item_Qty"]);
                    itemPrice = Convert.ToInt32(reader["Item_Price"]);
                }

                labelTotal.Content = "Total: 0 LKR";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Validation for text changes in Qty
        private void textBoxBuyQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Set Colour to normal if it changed to red in prevoius text changed
            labelTotal.Foreground = Brushes.Black;

            try
            {
                //Validaing the input of textBoxQty
                if (!String.IsNullOrWhiteSpace(textBoxBuyQty.Text) && textBoxBuyQty.Text.All(char.IsDigit))
                {
                    buyQty = Convert.ToInt32(textBoxBuyQty.Text);

                    if (buyQty > 0 && buyQty <= itemQtyLeft)
                    {
                        total = buyQty * itemPrice;
                        labelTotal.Content = "Total: " + total + " LKR";
                        qtyReady = true;
                    }
                    else
                    {
                        buyQty = 0;
                        labelTotal.Foreground = Brushes.Red;
                        labelTotal.Content = "Invalid Quantity !";
                        qtyReady = false;
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(textBoxBuyQty.Text))
                    {
                        buyQty = 0;
                        labelTotal.Foreground = Brushes.Red;
                        labelTotal.Content = "Enter Quantity !";
                        qtyReady = false;
                    }
                    else 
                    {
                        buyQty = 0;
                        labelTotal.Foreground = Brushes.Red;
                        labelTotal.Content = "Invalid Quantity !";
                        qtyReady = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Button Clicks
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (addSuccess == true)
            {
                this.Close();
            }

            if (qtyReady == true && addSuccess == false)
            {
                try
                {
                    // INsert Into Cart Table
                    string sqlAddToCart = "INSERT INTO Cart (Cart_ID, Item_ID, Cart_Qty, Price, Cus_ID) " +
                                        $"VALUES ('{IDgenarate.createID("Cart_ID")}', '{itemID}', '{buyQty}', '{total}', '{cusID}') ";
                    
                    SqlCommand cmdAddToCart = new SqlCommand(sqlAddToCart, con.GetDBconnetion());
                    cmdAddToCart.ExecuteNonQuery();

                    // Remove Items and display suxcces msg
                    container.Children.Remove(stackpanelQty);
                    buttonSubmit.Content = "OK";

                    Label labelMsg = new Label {
                        Content = "Successfully Added !",
                        FontFamily = new FontFamily("Segoe UI Variable Display"),
                        FontSize = 19,
                        Margin = new(0,65,0,0),
                        Foreground = Brushes.Blue,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    container.Children.Add(labelMsg);
                    addSuccess = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occur while add to cart\n\n"+ex.Message);
                }
            }
        }
    }
}
