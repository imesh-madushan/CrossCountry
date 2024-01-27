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
    
    public partial class MainWindow : Window
    {
        CreateDBconnection con = new CreateDBconnection();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserValidating(string username, string password)
        {
            int cusAvailability = 999;
            string cusID;

            try
            {   
                //Check for username and password match
                string sqlcheck = $"SELECT COUNT(*) FROM Customer WHERE Cus_Username = '{username}' AND Cus_Password = '{password}'";
                SqlCommand cmdcheck = new SqlCommand(sqlcheck, con.GetDBconnetion());
                cusAvailability = Convert.ToInt32(cmdcheck.ExecuteScalar());


                //If one result was found after ExecuteScalar
                if (cusAvailability == 1)
                {
                    //Get ther Cus_ID from that user 
                    string sqlgetid = $"SELECT Cus_Id FROM Customer WHERE Cus_Username = '{username}' AND Cus_Password = '{password}'";
                    SqlCommand cmdgetid = new SqlCommand(sqlgetid, con.GetDBconnetion());
                    cusID = cmdgetid.ExecuteScalar().ToString();
                   
                    //Opening Shopping window after succesful login
                    ShoppingWindow spwindow = new ShoppingWindow();

                    //Pass username and it's Cus_ID Shopping window as parameters
                    spwindow.SetLoggedUserDetail(username, cusID); 
                    spwindow.Show();
                    this.Close();
                }
                else
                {
                    lableLoginValidate.Content = "Invalid Login !";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Buton Clicks
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBoxUsername.Text) && !String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
            {
                string username = TextBoxUsername.Text;
                string password = PasswordBoxPassword.Password;
                UserValidating(username, password);
            }
            else
            {
                lableLoginValidate.Content = "Username or Password cannot be empty !";
            }            
        }


        private void ButtonRegistor_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow windowCaller = new RegisterWindow();
            windowCaller.Show();
            this.Close();
        }
        private void ButtonAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            AdminLoginWindow windowCall = new AdminLoginWindow();
            windowCall.Show();
            this.Close();
        }


        //Mouse Enter and Leave
        private void ButtonLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black ;
                button.Foreground = Brushes.White;
            }
        }

        private void ButtonLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void ButtonRegistor_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black;
                button.Foreground = Brushes.White;
            }
        }

        private void ButtonRegistor_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void TextBoxUsername_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                textbox.BorderBrush = Brushes.MediumBlue;
            }
        }

        private void TextBoxUsername_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                textbox.BorderBrush = Brushes.White;
            }
        }

        private void PasswordBoxPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pbox)
            {
                pbox.BorderBrush = Brushes.MediumBlue;
            }
        }

        private void PasswordBoxPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pbox)
            {
                pbox.BorderBrush = Brushes.White;
            }
        }

        

        private void ButtonAdminLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = Brushes.Black;
                button.Foreground = Brushes.Red;
                button.BorderThickness = new Thickness(1.4);
            }
        }

        private void ButtonAdminLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 41, 24, 112));
                button.Foreground = new SolidColorBrush(Color.FromArgb(255, 73, 30, 137));
            }
        }


        //Text changed
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lableLoginValidate.Content = string.Empty;
        }
    }
}
