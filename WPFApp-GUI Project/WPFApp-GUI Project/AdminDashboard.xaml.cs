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
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        string itemName = "I003";
        int itemQty = 23;
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void textBoxItemID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxItemID.Text == "I003")//remove this line used for testing
            {
                borderAadminAddItem.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemName}.jpg", UriKind.Absolute)));
                labelQty.Content = $"{itemQty} Left";
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
        //Button Click
        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowCall = new MainWindow();
            windowCall.Show();
            this.Close();
        }
        private void ButtonDashboard_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void ButtonSidebarAny_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.LightBlue;
            }
        }
        private void ButtonSidebarAny_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Transparent;
            }
        }
    }
}
