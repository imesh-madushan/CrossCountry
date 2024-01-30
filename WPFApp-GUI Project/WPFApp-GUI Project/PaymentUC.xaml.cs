using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
using static System.Net.Mime.MediaTypeNames;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for PaymentUC.xaml
    /// </summary>
    public partial class PaymentUC : UserControl
    {
        CreateDBconnection con = new CreateDBconnection();

        private string cusID;
        string dateTime;

        string itemName = "I002";
        int itemPrice;
        int total;
        int itemQtyLeft;
        int buyQty;

        bool cardNumReady = false;
        bool cardExpDateReady = false;
        bool cardCvvReady = false; 
        bool qtyReady = true;

        string paymentMsg;
        int msgColorFlag = -1;


        public PaymentUC()
        {
            InitializeComponent();
            setComboBoxAddress();
        }

        //Set Items to display
        public void setItem(string itemID)
        {
            itemName = itemID;

            try
            {
                borderBuyItem.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemID}.jpg", UriKind.Absolute)));
                
                //Reading Item information from database
                string sqlReadItem = $"SELECT * FROM Items WHERE Item_ID = '{itemID}'";
                SqlCommand cmdReadItem = new SqlCommand(sqlReadItem, con.GetDBconnetion());
                SqlDataReader reader = cmdReadItem.ExecuteReader();

                if (reader.Read())
                {
                    itemQtyLeft = Convert.ToInt32(reader["Item_Qty"]);
                    itemPrice = Convert.ToInt32(reader["Item_Price"]);
                }

                labelQtyLeft.Content = itemQtyLeft + " Left";
                labelTotal.Content = "Total: " + itemPrice + " LKR";
                textBoxBuyQty.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Add address to comboBoxAddress
        public void setComboBoxAddress()
        {
            //Get logged customer ID from ShoppingWindow
            //// cusID = ShoppingWindow.getLoggedID();
            cusID = "CusTEX09L6VA";
            
            //Read and Add customer address to datebase
            string sqlReadAddress = $"SELECT Address FROM Customer_Address WHERE Cus_ID = '{cusID}' ";

            try
            {
                SqlCommand cmdAddressReader = new SqlCommand(sqlReadAddress, con.GetDBconnetion());
                SqlDataReader reader = cmdAddressReader.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxAddress.Items.Add(reader["Address"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private void saveOder()
        {
            DateTime dt = DateTime.Now;

            string sql = "INSERT INTO Orrder (Order_ID, Order_Address, Bill, Oder_Date, Cus_ID) " +
                         $"VALUES ('{IDgenarate.createID("Order_ID")}', '{textBoxAddress.Text}', '{total}', '{dt}', '{cusID}')";

            try
            {
                SqlCommand cmdSaveOder = new SqlCommand(sql, con.GetDBconnetion());
                cmdSaveOder.ExecuteNonQuery();
                con.GetDBconnetion().Close();
                msgColorFlag = 0;
            }
            catch(Exception ex)
            {
                msgColorFlag = 1;
                MessageBox.Show(ex.Message);
            }
        }
        //Button Clicks
        private void buttonPay_Click(object sender, RoutedEventArgs e)
        {
            //Validating if all required data has inserted
            if (qtyReady == true)
            {
                if (!String.IsNullOrWhiteSpace(textBoxAddress.Text))
                {
                    if (cardNumReady == true && cardExpDateReady == true && cardCvvReady == true)
                    {
                        if (textBoxCardNum.Text.Length < 12 || textBoxExp.Text.Length < 5 || textBoxCvv.Text.Length < 3)
                        {
                            msgColorFlag = 1;
                            paymentMsg = "\nPayment failed due to Invalid payment details !";
                        }
                        else
                        {
                            saveOder();
                            paymentMsg = "\nPayment Succesfull";
                        }
                    }
                    else
                    {
                        msgColorFlag = 1;
                        paymentMsg = "\nPayment failed due to Invalid payment details !";
                    }
                }
                else
                {
                    msgColorFlag = 1;
                    paymentMsg = "\nPayment failed Please select Shipping Address !";
                }
            }
            else
            {
                msgColorFlag = 1;
                paymentMsg = "\nPayment failed due to Invalid Quntity !";
            }
            
            MsgWindow msgWindow = new MsgWindow();
            if (msgColorFlag == 0)
            {
                msgWindow.textBoxMsg.Foreground = Brushes.Blue;
            }
            else if (msgColorFlag == 1)
            {
                msgWindow.textBoxMsg.Foreground = Brushes.Red;
            }
            msgWindow.textBoxMsg.Text = paymentMsg;
            msgWindow.Show();
        }

        //Button Enter and Leaves
        private void buttonPay_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(1.5);
            }
        }

        private void buttonPay_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 95, 237, 203));
                button.BorderThickness = new Thickness(1.8);
            }
        }

        //Text Changes
        private void textBoxBuyQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is  TextBox) 
            {
                //Set Colour to normal if it changed to red in prevoius text changed
                labelTotal.Foreground = Brushes.Black;

                try
                {
                    //Validaing the input of textBoxQty
                    if (!String.IsNullOrWhiteSpace(textBoxBuyQty.Text) && textBoxBuyQty.Text.All(char.IsDigit) && Convert.ToInt32(textBoxBuyQty.Text) <= itemQtyLeft && Convert.ToInt32(textBoxBuyQty.Text) > 0)
                    {
                        buyQty = Convert.ToInt32(textBoxBuyQty.Text);
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
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);       
                }
            }
        }

        private void textBoxCardNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox) 
            {
                if (!String.IsNullOrWhiteSpace(textBoxCardNum.Text))
                {
                    if (textBoxCardNum.Text.All(char.IsDigit))
                    {
                        labelCnumValid.Content = "";
                        cardNumReady = true;
                    }
                    else
                    {
                        labelCnumValid.Content = "!";
                        cardNumReady = false;
                    }
                }
            }
        }

        private void textBoxExp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (!String.IsNullOrWhiteSpace(textBoxExp.Text))
                {
                    if (textBoxExp.Text.Length == 5)
                    {
                        if (textBoxExp.Text[2] == '/' && textBoxExp.Text.Substring(0, 2).All(char.IsDigit) && textBoxExp.Text.Substring(3).All(char.IsDigit))
                        {
                            cardExpDateReady = true;
                            labelExpValid.Content = "";
                        }
                        else
                        {
                            cardExpDateReady = false;
                            labelExpValid.Content = "!";
                        }
                    }
                    else
                    {
                        labelExpValid.Content = "";
                    }
                }
            }
        }

        private void textBoxCvv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (!String.IsNullOrWhiteSpace(textBoxCvv.Text))
                {
                    if (textBoxCvv.Text.All(char.IsDigit))
                    {
                        labelCvvValid.Content = "";
                        cardCvvReady = true;
                    }
                    else
                    {
                        labelCvvValid.Content = "!";
                        cardCvvReady &= false;
                    }
                }
            }
        }

        private void comboBoxAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxAddress.SelectedIndex != 0 && sender is ComboBox cmbox) 
            {
                textBoxAddress.Text = comboBoxAddress.SelectedItem.ToString();
                comboBoxAddress.SelectedIndex = 0;
            }
        }
    }
}
