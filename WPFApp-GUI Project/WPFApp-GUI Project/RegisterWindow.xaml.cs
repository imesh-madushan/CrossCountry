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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WPFApp_GUI_Project
{
    
    public partial class RegisterWindow : Window
    {
        CreateDBconnection con = new CreateDBconnection();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private string genarateCusID()
        {
            int idcount = 999;

            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder cusID = new StringBuilder("C", 10);  //Creating string using stringbuilder

            while (idcount != 0)
            {
                while (cusID.Length < 10)
                {
                    cusID.Append(chars[rnd.Next(chars.Length)]); //Add random string to from chars to cusID 
                }

                try
                {
                    string sql = $"SELECT COUNT(*) FROM Customer WHERE Cus_ID = '{cusID.ToString()}'"; //Find if the genarated cusID is already available in the database
                    SqlCommand cmd = new SqlCommand(sql, con.GetDBconnetion());

                    idcount = (int)cmd.ExecuteScalar(); //Set the result to idcount in int format 

                    if (idcount == 0)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occured while genarating new customer ID\n" + ex.Message);
                }
            }
            return cusID.ToString();
        }

        //Button Clicks
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            string cID;
            string cName;
            int cAge;
            string cAddress;
            string cUsername;
            string cPassword;
            string cCountry;
            
            try
            {
                //Validating entered data
                if (!String.IsNullOrWhiteSpace(textBoxName.Text) && !String.IsNullOrWhiteSpace(textBoxAge.Text) && !String.IsNullOrWhiteSpace(textBoxAddress.Text) && !String.IsNullOrWhiteSpace(textBoxUsername.Text) && !String.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    //Check if a country is not selected
                    if (comboBoxCountry.SelectedIndex != 0)
                    {
                        //Check for the length of the username and the password
                        if (textBoxUsername.Text.Length >= 8 && textBoxPassword.Text.Length >= 8)
                        {
                            //Insert data in to database
                            cID = genarateCusID();
                            cName = textBoxName.Text;
                            cAge = Convert.ToInt32(textBoxAge.Text);
                            cAddress = textBoxAddress.Text;
                            cUsername = textBoxUsername.Text;
                            cPassword = textBoxPassword.Text;

                            string sql = "INSERT INTO Customer (Cus_ID, Cus_Username, Cus_Name, Cus_Age, Cus_Password)" +
                                 $"VALUES ('{cID}', '{cUsername}', '{cName}', '{cAge}', '{cPassword}')";

                            SqlCommand cmd = new SqlCommand(sql, con.GetDBconnetion());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registration success");
                        }
                        else 
                        {
                            labelValidateDetails.Content = "Username and Password must contain atleast 8 characters ";
                        }
                    }
                    else
                    {
                        labelValidateDetails.Content = "Please select country !";
                    }
                    
                }
                else
                {
                    labelValidateDetails.Content = "Please fill all feilds to continue !";
                }
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //Mouse Enter and Leaves
        private void textBoxUsername_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                textbox.BorderBrush = Brushes.MediumBlue;
            }
        }

        private void textBoxUsername_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                textbox.BorderBrush = Brushes.White;
            }
        }

        private void buttonLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black;
                button.Foreground = Brushes.White;
            }
        }

        private void buttonLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void buttonRegistor_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black;
                button.Foreground = Brushes.White;
            }
        }

        private void buttonRegistor_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
