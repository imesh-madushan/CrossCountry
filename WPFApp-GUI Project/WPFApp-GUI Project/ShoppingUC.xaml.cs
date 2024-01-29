using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for ShoppingUC.xaml
    /// </summary>
    public partial class ShoppingUC : UserControl
    {
        //admin or customer
        string type;
        CreateDBconnection getconobj = new CreateDBconnection();

        public ShoppingUC()
        {
            InitializeComponent();
           // ReadDBItemData();
        }

        public void Creater(string userType)
        {
            type = userType;
            ReadDBItemData();
        }
        private void ReadDBItemData()
        {
            string itemName;
            int price;
            int quantity;

            string sqlRead = "SELECT Item_ID, Item_Price, Item_Qty from Items";

            try
            {
                SqlCommand cmdobj = new SqlCommand(sqlRead, getconobj.GetDBconnetion());
                SqlDataReader reader = cmdobj.ExecuteReader();

                while (reader.Read())
                {
                    itemName = Convert.ToString(reader["Item_ID"]);
                    price = Convert.ToInt32(reader["Item_Price"]);
                    quantity = Convert.ToInt32(reader["Item_Qty"]);

                    CreateItems(itemName, price, quantity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while raeding data from database\nItems will not be displayed !\n" + ex.Message);
            }

        }

        //Creat available Item list
        public void CreateItems(string itemName, int price, int quantity)
        {
            try
            {
                //creating grid for allocate spaces to items in wrappanel
                Grid itemGrid = new Grid
                {
                    Name = $"{itemName}",
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
                    Source = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemGrid.Name}.jpg", UriKind.Absolute)),
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
                    Content = $"Qty {quantity}",
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
                    Style = (Style)FindResource("RoundedButtonBuyStyle"),
                    FontFamily = new FontFamily("Segoe UI Variable Small Semibold"),
                    FontSize = 11,
                    BorderThickness = new Thickness(0, 0, 0, 0)
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
                itemGrid.Children.Add(outlineBorder);
                itemGrid.Children.Add(itemImage);
                itemGrid.Children.Add(priceLable);
                itemGrid.Children.Add(qtyLable);
                

                if (type == "admin")
                {
                    itemImage.Margin = new(0,46,0,0);
                }
                else if (type == "customer")
                {
                    itemGrid.Children.Add(buyNowButton);
                    itemGrid.Children.Add(addToCartButton);
                }
                
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
                    
                    PaymentUC pay = new PaymentUC();

                    pay.setItem(gridName);

                    //Display payment usercontrol
                    if (Window.GetWindow(this) is ShoppingWindow shoppingWindow)
                    {
                        shoppingWindow.ContentDisplay.Children.Clear();
                        shoppingWindow.ContentDisplay.Children.Add(pay);
                    }
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

        //Button Enter and Leaves
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
