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
        int hint = 0;

        private string cID;
        private string addID;
        private string cName;
        private int cAge;
        private string cAddress;
        private string cUsername;
        private string cPassword;
        private string cCountry;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        //Button Clicks
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            //Set things to normal if they were modified from previous Register button Click
            buttonRegister.Margin = new(0, 0, 0, 0);
            textBlocklValidateDetails.Text = String.Empty;

            try
            {
                //Validating entered data
                if (!String.IsNullOrWhiteSpace(textBoxName.Text) && !String.IsNullOrWhiteSpace(textBoxAge.Text) && !String.IsNullOrWhiteSpace(textBoxAddress.Text) && !String.IsNullOrWhiteSpace(textBoxUsername.Text) && !String.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    //Check for the length of the username and the password
                    if (textBoxUsername.Text.Length >= 8 && textBoxPassword.Text.Length >= 8)
                    {
                        //Check if a country is selected
                        if (comboBoxCountry.SelectedIndex != 0)
                        {
                            int usernameHits = -1;

                            //Insert data in to database
                            cID = IDgenarate.createID("Cus_ID");
                            addID = IDgenarate.createID("Address_ID");
                            cName = textBoxName.Text;
                            cAge = Convert.ToInt32(textBoxAge.Text);
                            cAddress = textBoxAddress.Text;
                            cUsername = textBoxUsername.Text;
                            cPassword = textBoxPassword.Text;

                            //Get the combobox country
                            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxCountry.SelectedItem;
                            cCountry = selectedItem.Content.ToString();

                            //Check for if username already taken
                            string sqlCheckUsername = $"SELECT COUNT(*) FROM Customer WHERE Cus_Username = '{cUsername}'";
                            SqlCommand cmdCheckusername = new SqlCommand(sqlCheckUsername, con.GetDBconnetion());
                            usernameHits = (int)cmdCheckusername.ExecuteScalar();

                            if (usernameHits == 0)
                            {
                                string sqlInsertAllData = "INSERT INTO Customer (Cus_ID, Cus_Username, Cus_Name, Cus_Age, Cus_Password, Cus_Country)" +
                                 $"VALUES ('{cID}', '{cUsername}', '{cName}', '{cAge}', '{cPassword}', '{cCountry}') ";

                                string sqlInsertAddress = "INSERT INTO Customer_Address (Cus_ID, Address_ID, Address)" +
                                                           $"VALUES ('{cID}', '{addID}', '{cAddress}') ";

                                SqlCommand cmdAllData = new SqlCommand(sqlInsertAllData+sqlInsertAddress, con.GetDBconnetion());
                                cmdAllData.ExecuteNonQuery();

                                MsgWindow msgWindow = new MsgWindow();
                                msgWindow.Height = 240;
                                msgWindow.Width = 250;
                                msgWindow.textBoxMsg.Foreground = Brushes.Blue;
                                msgWindow.textBoxMsg.Text = "Registration Success !\n\nPlease Login to Continue";
                                msgWindow.Show();
                                //MessageBox.Show("Registration success");
                            }
                            else
                            {
                                textBlocklValidateDetails.Text = "Username has already taken !";
                            }
                            
                        }
                        else
                        {
                            textBlocklValidateDetails.Text = "Please select country !";
                        }
                    }
                    else
                    {
                        if (textBoxUsername.Text.Length < 8)
                        {
                            textBlocklValidateDetails.Text = "Username must contain atleast 8 characters ";
                        }

                        if (textBoxPassword.Text.Length < 8)
                        {
                            textBlocklValidateDetails.Text = "Password must contain atleast 8 characters ";
                        }

                        if (textBoxUsername.Text.Length < 8 && textBoxPassword.Text.Length < 8)
                        {
                            buttonRegister.Margin = new(0, 15, 0, -15);
                            textBlocklValidateDetails.Text = "Password and Username must contain atleast 8 characters ";
                        }
                    }
                }
                else
                {
                    textBlocklValidateDetails.Text = "Please fill all feilds to continue !";
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

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
