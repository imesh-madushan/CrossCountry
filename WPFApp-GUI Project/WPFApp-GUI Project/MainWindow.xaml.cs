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
        string usename;
        string password;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            usename = textBoxUsername.Text;
            password = passwordBoxPassword.Password;

        }
        private void buttonRegistor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(usename + password);
        }

        private void buttonLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black ;
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

        private void passwordBoxPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pbox)
            {
                pbox.BorderBrush = Brushes.MediumBlue;
            }
        }

        private void passwordBoxPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pbox)
            {
                pbox.BorderBrush = Brushes.White;
            }
        }
    }
}
