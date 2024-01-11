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
using System.Windows.Shapes;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
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
