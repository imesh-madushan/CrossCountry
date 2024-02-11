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
        private string loginType = "";

        private string cusID;
        private int cusAvailability = -1;

        private string adminID;
        private int adminAvailability = -1;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        // Validating Login info of Customer
        private void UserValidating(string username, string password)
        {
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
                }
                else
                {
                    lableLoginValidate.Content = "Invalid User Login !";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Validating Login info of Admin
        private void AdminValidating(string username, string password)
        {


            try
            {
                //Check for username and password match
                string sqlcheck = $"SELECT COUNT(*) FROM Admin WHERE Admin_Username = '{username}' AND Admin_Password = '{password}'";
                SqlCommand cmdcheck = new SqlCommand(sqlcheck, con.GetDBconnetion());
                adminAvailability = Convert.ToInt32(cmdcheck.ExecuteScalar());

                //If one result was found after ExecuteScalar
                if (adminAvailability == 1)
                {
                    //Get ther Cus_ID from that user 
                    string sqlgetid = $"SELECT Admin_ID FROM Admin WHERE Admin_Username = '{username}' AND Admin_Password = '{password}'";
                    SqlCommand cmdgetid = new SqlCommand(sqlgetid, con.GetDBconnetion());
                    adminID = cmdgetid.ExecuteScalar().ToString();
                }
                else
                {
                    lableLoginValidate.Content = "Invalid Admin Login !";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Buton Clicks
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            // Check if input are not null 
            if (!String.IsNullOrWhiteSpace(TextBoxUsername.Text) && !String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
            {
                string username = TextBoxUsername.Text;
                string password = PasswordBoxPassword.Password;
                UserValidating(username, password);

                if (cusAvailability == 1)
                {
                    //Store logged user details in Shopping window
                    ShoppingWindow.SetLoggedUserDetail(username, cusID);

                    //Opening Shopping window after succesful login
                    ShoppingWindow spwindow = new ShoppingWindow();
                    spwindow.Show();

                    this.Close();
                }
            }
            else
            {
                //Set error message if username and password are not matched
                lableLoginValidate.Content = "Username or Password cannot be empty !";
            }            
        }


        private void ButtonRegistor_Click(object sender, RoutedEventArgs e)
        {
            //Go to the register window
            RegisterWindow windowCaller = new RegisterWindow();
            windowCaller.Show();
            this.Close();
        }
        private void ButtonAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            // Check if the inputs are not null
            if (!String.IsNullOrWhiteSpace(TextBoxUsername.Text) && !String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
            {
                string username = TextBoxUsername.Text;
                string password = PasswordBoxPassword.Password;
                AdminValidating(username, password);

                if (adminAvailability == 1)
                {
                    //Store logged Admin details in Shopping window
                    AdminDashboard.SetLoggedAdminDetail(username, adminID);

                    //Opening Admin Dashboard window after succesful login
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.Show();

                    this.Close();
                }
            }
            else
            {
                //Set error message if username and password are not matched
                lableLoginValidate.Content = "Invalid Admin Login !";
            }
        }


        //Mouse Enter and Leave
        private void ButtonLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            //Change the color of button
            if (sender is Button button)
            {
                button.Background = Brushes.Black ;
                button.Foreground = Brushes.White;
            }
        }

        private void ButtonLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            //Reset the color of button
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void ButtonRegistor_MouseEnter(object sender, MouseEventArgs e)
        {
            //Change the color of button
            if (sender is Button button)
            {
                button.Background = Brushes.Black;
                button.Foreground = Brushes.White;
            }
        }

        private void ButtonRegistor_MouseLeave(object sender, MouseEventArgs e)
        {
            //Reset the color of button
            if (sender is Button button)
            {
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
        }

        private void TextBoxUsername_MouseEnter(object sender, MouseEventArgs e)
        {
            //Change the color of ther boder of textbox
            if (sender is TextBox textbox)
            {
                textbox.BorderBrush = Brushes.MediumBlue;
            }
        }

        private void TextBoxUsername_MouseLeave(object sender, MouseEventArgs e)
        {
            //Reset the color of ther boder of textbox
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
            //Change the color of button
            if (sender is Button button)
            {
                button.BorderBrush = Brushes.Black;
                button.Foreground = Brushes.Red;
                button.BorderThickness = new Thickness(1.4);
            }
        }

        private void ButtonAdminLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            //Reset the color of button
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

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
