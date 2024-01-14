using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        string username;
        string password;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            username = TextBoxUsername.Text;
            password = PasswordBoxPassword.Password;

        }
        //Buton Clicks
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
    }
}
