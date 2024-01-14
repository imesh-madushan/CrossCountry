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

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        int itemCount = 1;
        int price = 2000;
        int quantity = 23;
        public Dashboard()
        {
            InitializeComponent();

            //creating items.
            try
            {
                //creating grid for allocate spaces to items in wrappanel
                Grid itemGrid = new Grid
                {
                    Name = $"I{itemCount}",
                    Width = 233,
                    Height = 190
                };

                //creating border which has green color
                Border outlineBorder = new Border
                {
                    CornerRadius = new(15),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(255, 34, 184, 134)),
                    BorderThickness = new(2),
                    Margin = new(20, 25, 20, 0)
                };

                //creating item image
                Image itemImage = new Image
                {
                    Source = new BitmapImage(new Uri($"/products/{itemGrid.Name}")),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    RenderTransformOrigin = new(0.5, 0.5),
                    Width = 151,
                    Height = 136,
                    Stretch = Stretch.UniformToFill,
                    Margin = new(0, 30, 0, 0)
                };

                //creating lable for display price
                Label priceLable = new Label
                {
                    Name = $"lablePrice{itemGrid.Name}",
                    Content = $"LKR {price}",
                    Margin = new(33, 34, 102, 126),
                    FontFamily = new FontFamily("Segoe UI Variable Small Semibold"),
                    FontSize = 14,
                };

                //creating lable for display available quantity
                Label qtyLable = new Label
                {
                    Name = $"lableQty{itemGrid.Name}",
                    Content = $"Qty{quantity}",
                    Margin = new(144, 32, 17, 127),
                    FontFamily = new FontFamily("Segoe UI Variable Small Semibold"),
                    FontSize = 14,
                };

                //creating Buy Now button
                Button buyNowButton = new Button
                {
                    Name = $"buttonBuyNow{itemGrid.Name}",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new(36, 151, 0, 0),
                    Width = 72,
                    Height = 26,
                    Style = (Style)FindResource("RoundedButtonBuyStyles"),
                    FontFamily = new FontFamily("Segoe UI Variable Small Semibold"),
                    FontSize = 11,
                    BorderThickness = new Thickness(0,0,0,0)                    
                };
                buyNowButton.Click += BuyButton_Click;
                buyNowButton.MouseEnter += ButtonBuyNow_MouseEnter;
                buyNowButton.MouseLeave += ButtonBuyNow_MouseLeave;

                //creating add to cart button
                Button addToCartButton = new Button
                {
                    Height = 25,
                    Style = (Style)FindResource("PngButtonStyle"),
                    Margin = new(160, 151, 39, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    BorderBrush = Brushes.White,
                    Foreground = Brushes.White,
                    Background = Brushes.White,
                };
                addToCartButton.Click += AddToCartButton_Click;

                //add all abaove created deature to wrappanel
                wrapPanelItems.Children.Add(itemGrid);
                wrapPanelItems.Children.Add(outlineBorder);
                wrapPanelItems.Children.Add(itemImage);
                wrapPanelItems.Children.Add(priceLable);
                wrapPanelItems.Children.Add(qtyLable);
                wrapPanelItems.Children.Add(buyNowButton);
                wrapPanelItems.Children.Add(addToCartButton);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while creating items");
                throw;
            }

            
        }

        //Button Clicks
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (clickedButton.Parent is Grid clickedGrid)
                {
                    string gridName = clickedGrid.Name;

                    MessageBox.Show($"Buy button clicked in Grid {gridName}");
                }
            }
        }
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
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

        private void ButtonBuyNow_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 34, 184, 134));
                button.BorderThickness = new Thickness(1.5);
            }
        }
        private void ButtonBuyNow_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderThickness = new Thickness(0);
            }
        }
    }
}
