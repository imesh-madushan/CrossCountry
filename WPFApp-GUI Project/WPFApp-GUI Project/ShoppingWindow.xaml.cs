using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Windows.Themes;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class ShoppingWindow : Window
    {
        private static string loggedUsername;
        private static string loggedUserID;
        
        public ShoppingWindow()
        {
            InitializeComponent();
            LoadDefault();

        }
        public void LoadDefault()
        {
            labelProfileUsername.Content = loggedUsername;

            ShoppingUC shoppingUC = new ShoppingUC();
            shoppingUC.Creater("customer");
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(shoppingUC);
        }

        public static void SetLoggedUserDetail(string username, string userID)
        { 
            loggedUsername = username;
            loggedUserID = userID;
        }

        public static string getLoggedID()
        { 
            return loggedUserID;
        }
        public static string getLoggedUsername()
        {
            return loggedUsername;
        }

        //Button Clicks
        private void ButtonDashboard_Click(object sender, RoutedEventArgs e)
        {
            LoadDefault();
        }
        private void ButtonProfileSetting_Click(object sender, RoutedEventArgs e)
        {
            CustomerSettingsUC customerSettingsUC = new CustomerSettingsUC();
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(customerSettingsUC);
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryUC historyUC = new HistoryUC();
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(historyUC); 
        }

        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
            CartUC cartUC = new CartUC();
            ContentDisplay.Children.Clear();
            ContentDisplay.Children.Add(cartUC);
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowCall = new MainWindow();
            windowCall.Show();
            this.Close();
        }

        //Mouse Enter Leave Animations
        private void ButtonLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(160, 255, 20, 2));
            }
        }
        private void ButtonLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Transparent;
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
