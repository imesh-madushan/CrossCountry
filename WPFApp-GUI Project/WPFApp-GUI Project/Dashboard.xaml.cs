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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void buttonLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(160, 255, 20, 2));
            }
        }

        private void buttonLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Transparent;
            }
        }

        private void buttonProfileSetting_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.Background = Brushes.LightBlue;
            }
        }

        private void buttonProfileSetting_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Transparent;
            }
        }

        private void buttonCart_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.Background = Brushes.LightBlue;
            }
        }

        private void buttonCart_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.Background = Brushes.Transparent;
            }
        }

        private void buttonHistpry_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.Background = Brushes.LightBlue;
            }
        }

        private void buttonHistpry_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            { 
                button.Background = Brushes.Transparent;
            }
        }

        private void buttonProfileSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (sender is FrameworkElement clickedButton)
            {
                // Access the parent grid using the Parent property
                if (clickedButton.Parent is Grid clickedGrid)
                {
                    // Access the Name property of the clicked grid
                    string gridName = clickedGrid.Name;

                    // Perform actions based on the clicked grid name
                    MessageBox.Show($"Buy button clicked in Grid {gridName}");
                }
            }*/
            if (sender is Button clickedButton)
            {
                if (clickedButton.Parent is Grid clickedGrid)
                {
                    string gridName = clickedGrid.Name;
                    MessageBox.Show($"Buy button clicked in Grid {gridName}");
                }
            }
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (clickedButton.Parent is Grid clickedGrid)
                {
                    string gridName = clickedGrid.Name;
                    MessageBox.Show($"Cart button clicked in Grid {gridName}");
                }
            }
        }
    }
}
