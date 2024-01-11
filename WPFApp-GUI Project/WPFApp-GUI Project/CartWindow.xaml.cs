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
            newItemBorder.BorderThickness = new Thickness(2);
            newItemBorder.CornerRadius = new CornerRadius(50);
            newItemBorder.Height = 128;
            newItemBorder.Width = 700;
            newItemBorder.Margin = new Thickness(0, 21, 0, 0);
            newItemBorder.Background = new SolidColorBrush(Color.FromRgb(71, 170, 159)); // #FF47AA9F

            // Create a new StackPanel within the Border
            StackPanel newItemStackPanel = new StackPanel();
            newItemStackPanel.Orientation = Orientation.Horizontal;
            newItemStackPanel.Width = 720;
            newItemStackPanel.Margin = new Thickness(0, 0, -24, -2);

            // Create a new Border within the StackPanel for the image
            Border newImageBorder = new Border();
            newImageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(71, 35, 192)); // #FF4723C0
            newImageBorder.BorderThickness = new Thickness(2);
            newImageBorder.Width = 105;
            newImageBorder.Height = 105;
            newImageBorder.CornerRadius = new CornerRadius(48);
            newImageBorder.Margin = new Thickness(20, 0, 0, 2);
            newImageBorder.VerticalAlignment = VerticalAlignment.Center;

            // Set the image source for the new Border
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("s (12).jpg", UriKind.Relative));
            newImageBorder.Background = imageBrush;

            // Add the new Border to the StackPanel
            newItemStackPanel.Children.Add(newImageBorder);

            // Create a new Label within the StackPanel
            Label newLabel = new Label();
            newLabel.Content = "Quantity " + itemCount;
            newLabel.Height = 38;
            newLabel.Margin = new Thickness(80, 0, 0, 0);
            newLabel.VerticalAlignment = VerticalAlignment.Center;
            newLabel.FontSize = 20;
            newLabel.Width = 114;
            newLabel.FontFamily = new FontFamily("Calibri");

            // Add the new Label to the StackPanel
            newItemStackPanel.Children.Add(newLabel);

            // Create a new Button within the StackPanel for Buy Now
            Button newBuyNowButton = new Button();
            newBuyNowButton.Style = FindResource("RoundedButtonStyle") as Style;
            newBuyNowButton.Content = "Buy Now";
            newBuyNowButton.Height = 37;
            newBuyNowButton.Width = 115;
            newBuyNowButton.Margin = new Thickness(80, 0, 0, 0);
            newBuyNowButton.BorderBrush = null;
            newBuyNowButton.VerticalAlignment = VerticalAlignment.Center;
            newBuyNowButton.FontSize = 19;
            newBuyNowButton.Background = Brushes.White;
            newBuyNowButton.FontFamily = new FontFamily("Calibri");

            // Add the new Buy Now Button to the StackPanel
            newItemStackPanel.Children.Add(newBuyNowButton);

            // Create a new Button within the StackPanel for delete action
            Button newDeleteButton = new Button();
            newDeleteButton.Style = FindResource("RoundedButtonStyle") as Style;
            newDeleteButton.Background = null;
            newDeleteButton.Height = 48;
            newDeleteButton.BorderBrush = null;
            newDeleteButton.Width = 48;
            newDeleteButton.Margin = new Thickness(80, 0, 0, 0);

            // Create a new StackPanel within the delete Button
            StackPanel deleteButtonStackPanel = new StackPanel();
            deleteButtonStackPanel.Orientation = Orientation.Horizontal;
            deleteButtonStackPanel.Width = 55;

            // Set the image source for the delete Button
            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("delete (1).png", UriKind.Relative));
            deleteImage.Width = 47;
            deleteImage.Height = 53;

            // Add the new Image to the StackPanel within the delete Button
            deleteButtonStackPanel.Children.Add(deleteImage);

            // Set the StackPanel as the content of the delete Button
            newDeleteButton.Content = deleteButtonStackPanel;

            // Add the new delete Button to the StackPanel
            newItemStackPanel.Children.Add(newDeleteButton);

            // Set the StackPanel as the content of the Border
            newItemBorder.Child = newItemStackPanel;

            // Add the new Border to the existing StackPanel
            stackPanelCart.Children.Add(newItemBorder);
        }

        
    }
}

