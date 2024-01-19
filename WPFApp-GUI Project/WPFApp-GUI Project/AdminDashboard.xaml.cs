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
            LoadDefault();
        }

        //Button Click

        public void LoadDefault()
        {
            AdminUpdateStock adminUpdateStock = new AdminUpdateStock();
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(adminUpdateStock);
        }
        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowCall = new MainWindow();
            windowCall.Show();
            this.Close();
        }
        private void ButtonDashboard_Click(object sender, RoutedEventArgs e)
        {
            
            ShoppingUC shoppingUC = new ShoppingUC();
            shoppingUC.Creater("admin");
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(shoppingUC);
        }

        private void ButtonUpdateStock_Click(object sender, RoutedEventArgs e)
        {
            LoadDefault();
        }
        //Button Enter and Leave

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
