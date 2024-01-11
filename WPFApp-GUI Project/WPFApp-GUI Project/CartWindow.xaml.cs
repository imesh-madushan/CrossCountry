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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private int itemCount = 0;
        public CartWindow()
        {
            InitializeComponent();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            itemCount++;

            // Create a new Border
            Border newItemBorder = new Border();
            newItemBorder.BorderBrush = Brushes.Red;
            newItemBorder.BorderThickness = new Thickness(0);
            newItemBorder.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
            newItemBorder.CornerRadius = new CornerRadius(50);
            newItemBorder.Height = 140;
            newItemBorder.Width = 720;

            // Create a new StackPanel inside the Border
            StackPanel newItemStackPanel = new StackPanel();
            newItemStackPanel.Orientation = Orientation.Horizontal;
            newItemStackPanel.Height = 140;
            newItemStackPanel.Width = 720;

            // Create a new Border inside the StackPanel for the image
            Border imageBorder = new Border();
            imageBorder.BorderThickness = new Thickness(4);
            imageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(41, 143, 171));
            imageBorder.Width = 120;
            imageBorder.Height = 120;
            imageBorder.CornerRadius = new CornerRadius(38);
            imageBorder.Margin = new Thickness(20, 0, 0, 0);
            imageBorder.VerticalAlignment = VerticalAlignment.Center;

            // Create a new ImageBrush for the background of the inner Border

            // Create a new Label
            Label quantityLabel = new Label();
            quantityLabel.Content = "Quantity " + itemCount;
            quantityLabel.Height = 38;
            quantityLabel.Margin = new Thickness(80, 0, 0, 0);
            quantityLabel.VerticalAlignment = VerticalAlignment.Center;
            quantityLabel.FontSize = 15;

            // Create a new Button with the specified style
            Button buyNowButton = new Button();
            buyNowButton.Style = (Style)FindResource("RoundedButtonStyle");
            buyNowButton.Content = "Buy Now";
            buyNowButton.Height = 33;
            buyNowButton.Width = 112;
            buyNowButton.Margin = new Thickness(80, 0, 0, 0);
            buyNowButton.BorderBrush = null;
            buyNowButton.VerticalAlignment = VerticalAlignment.Center;
            buyNowButton.FontSize = 15;

            // Add the controls to the StackPanel
            newItemStackPanel.Children.Add(imageBorder);
            newItemStackPanel.Children.Add(quantityLabel);
            newItemStackPanel.Children.Add(buyNowButton);

            // Add the StackPanel to the Border
            newItemBorder.Child = newItemStackPanel;

            // Add the new Border to the existing StackPanel
            stackPanelCart.Children.Add(newItemBorder);
        }

        
    }
}

