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
    public partial class CartWindow : Window
    {
        private int itemCount = 1;
        public CartWindow()
        {
            InitializeComponent();
        }

        
        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {            
            Grid newGrid = new Grid();
            newGrid.Name = "I" + itemCount;

            string cartImagePath = $"{newGrid.Name}.jpg";

            // Create Border
            Border border = new Border();
            border.BorderThickness = new Thickness(2);
            border.CornerRadius = new CornerRadius(43);
            border.Height = 100;
            border.Width = 641;
            border.Margin = new Thickness(0, 21, 0, 0);
            SolidColorBrush borderBrushColor= new SolidColorBrush(Color.FromArgb(255, 226, 227, 219));
            border.Background = borderBrushColor;

            // Create StackPanel
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(42, 21, 42, -2);

            // Create ImageBrush
            ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/{cartImagePath}", UriKind.Absolute));
            imageBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{cartImagePath}", UriKind.Absolute));

            // Create Image Border
            Border imageBorder = new Border();
            SolidColorBrush imageBorderColor = new SolidColorBrush(Color.FromArgb(255, 95, 237, 203));
            imageBorder.BorderBrush = imageBorderColor;
            imageBorder.BorderThickness = new Thickness(2);
            imageBorder.Width = 83;
            imageBorder.Height = 82;
            imageBorder.CornerRadius = new CornerRadius(48);
            imageBorder.Margin = new Thickness(11, 0, 0, 2);
            imageBorder.VerticalAlignment = VerticalAlignment.Center;
            imageBorder.Background = imageBrush;
           

            // Create Labels
            Label label1 = new Label();
            label1.Content = "Quantity 3";
            label1.Height = 38;
            label1.Margin = new Thickness(30, 0, 0, 0);
            label1.VerticalAlignment = VerticalAlignment.Center;
            label1.FontSize = 19;
            label1.Width = 114;
            label1.FontFamily = new FontFamily("Calibri");

            Label label2 = new Label();
            label2.Content = "6000 LKR";
            label2.Height = 38;
            label2.Margin = new Thickness(30, 0, 0, 0);
            label2.VerticalAlignment = VerticalAlignment.Center;
            label2.FontSize = 19;
            label2.Width = 114;
            label2.FontFamily = new FontFamily("Calibri");

            // Create Buttons
            Button buyNowButton = new Button();
            buyNowButton.Style = (Style)FindResource("RoundedButtonStyle");
            buyNowButton.Name = "buttonCartBuyNow";
            buyNowButton.Content = "Buy Now";
            buyNowButton.Height = 37;
            buyNowButton.Width = 115;
            buyNowButton.Margin = new Thickness(30, 0, 0, 0);
            buyNowButton.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
            buyNowButton.BorderThickness = new Thickness(2);
            buyNowButton.VerticalAlignment = VerticalAlignment.Center;
            buyNowButton.FontSize = 18;
            buyNowButton.Background = new SolidColorBrush(Colors.White);
            buyNowButton.FontFamily = new FontFamily("Calibri");
            buyNowButton.Click += buttonCartBuyNow_Click;

            Button deleteButton = new Button();
            deleteButton.Style = (Style)FindResource("RoundedButtonStyle");
            deleteButton.Background = null;
            deleteButton.Height = 48;
            deleteButton.BorderBrush = null;
            deleteButton.Width = 48;
            deleteButton.Margin = new Thickness(45, 0, 0, 0);
            deleteButton.Click += buttonCartDelete_Click;

            StackPanel deleteButtonStackPanel = new StackPanel();
            deleteButtonStackPanel.Orientation = Orientation.Horizontal;
            deleteButtonStackPanel.Width = 55;

            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("/delete (1).png", UriKind.Relative));
            deleteImage.Width = 37;
            deleteImage.Height = 52;

            deleteButtonStackPanel.Children.Add(deleteImage);
            deleteButton.Content = deleteButtonStackPanel;
            

            // Add elements to StackPanel
            stackPanel.Children.Add(imageBorder);
            stackPanel.Children.Add(label1);
            stackPanel.Children.Add(label2);
            stackPanel.Children.Add(buyNowButton);
            stackPanel.Children.Add(deleteButton);

            // Add Border and StackPanel to Grid
            newGrid.Children.Add(border);
            newGrid.Children.Add(stackPanel);

            // Add the new Grid to your existing container (e.g., a parent Grid or a StackPanel)
            // For example, if your parent container is a Grid named "mainGrid":
            stackPanelCart.Children.Add(newGrid);

            // Increment the item counter for the next item
            itemCount++;

            if (sender is Button button && button.Parent is StackPanel sP && sP.Parent is Grid mainGrid)
            {
                string mainGridName = mainGrid.Name;
                MessageBox.Show("Clicked item: " + mainGridName);
            }
        }

        private void buttonCartBuyNow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is StackPanel stackpanel && stackpanel.Parent is Grid maingrid)
            {
                string maingridName = maingrid.Name;
                MessageBox.Show(maingridName);
            }
        }

        private void buttonCartDelete_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.Parent is StackPanel stackpanel && stackpanel.Parent is Grid maingrid)
            {
                stackPanelCart.Children.Remove(maingrid);
                //please add data base cart deletion tooo
            }
        }
    }
 }

