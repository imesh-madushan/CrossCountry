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
    /// Interaction logic for CartUC.xaml
    /// </summary>
    public partial class CartUC : UserControl
    {
        CreateDBconnection con = new CreateDBconnection();
        private string loggedUserID;
        public CartUC()
        {
            InitializeComponent();
            loggedUserID =  ShoppingWindow.getLoggedID();
            readCartFromDB();
        }

        // Read cart from Database relavent to user ID
        private void readCartFromDB()
        {
            string sqlReadCart = $"SELECT * FROM Cart WHERE Cus_ID = '{loggedUserID}'";

            try 
            {
                SqlCommand cmdReadCart = new SqlCommand(sqlReadCart, con.GetDBconnetion());
                SqlDataReader reader = cmdReadCart.ExecuteReader();

                while (reader.Read())
                {
                    string cartID = reader["Cart_ID"].ToString();
                    string itemName = reader["Item_ID"].ToString();
                    int cartQty = Convert.ToInt32(reader["Cart_Qty"]);
                    int cartPrice = Convert.ToInt32(reader["Price"]);

                    CreateCartItems(cartID, itemName, cartQty, cartPrice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occure while read Cart from Database\n\n"+ex.Message);
            }
        }

        // Create Cart items to Display
        private void CreateCartItems(string cartID, string itemName, int cartQty, double cartPrice)
        {
            // Create the main grid
            Grid cartGrid = new Grid
            {
                Name = cartID,
                Height = 129
            };

            // Create the outer border
            Border outerBorder = new Border
            {
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(43),
                Height = 102,
                Margin = new Thickness(115, 24, 114, 3),
                Background = new SolidColorBrush(Color.FromRgb(125, 241, 213))
            };

            // Create the stack panel
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(114, 21, 114, -2)
            };

            // Create the inner border with image
            Border innerBorder = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(95, 237, 203)),
                BorderThickness = new Thickness(2),
                Width = 83,
                Height = 82,
                CornerRadius = new CornerRadius(48),
                Margin = new Thickness(17, 0, 0, 2),
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create the image brush for the inner border
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemName}.jpg", UriKind.Absolute)),
            };

            innerBorder.Background = imageBrush;

            // Create quantity label
            Label quantityLabel = new Label
            {
                Content = $"Qty {cartQty}",
                Height = 38,
                Margin = new Thickness(45, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 19,
                Width = 66,
                FontFamily = new FontFamily("Calibri")
            };

            // Create totalprice label
            Label totalLabel = new Label
            {
                Content = $"Total: {cartPrice} LKR",
                Height = 38,
                Margin = new Thickness(45, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 19,
                Width = 150,
                FontFamily = new FontFamily("Calibri")
            };

            // Create Buy noe button
            Button buyNowButton = new Button
            {
                Content = "Buy Now",
                Height = 37,
                Width = 115,
                Style = (Style)FindResource("RoundedButtonStyle"),
                Margin = new Thickness(50, 0, 0, 0),
                BorderBrush = new SolidColorBrush(Colors.Aquamarine),
                BorderThickness = new Thickness(2),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18,
                Background = new SolidColorBrush(Color.FromRgb(253, 253, 253)),
                FontFamily = new FontFamily("Calibri")
            };

            // Create delete button
            Button deleteButton = new Button
            {
                Height = 48,
                Width = 48,
                Margin = new Thickness(68, 0, 0, 0),
                Background = null,
                BorderBrush = null,
            };
            StackPanel deleteButtonStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Width = 55
            };
            Image deleteImage = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/Icons/delete.png", UriKind.Absolute)),
                Width = 38,
                Height = 54
            };
            deleteButtonStackPanel.Children.Add(deleteImage);
            deleteButton.Content = deleteButtonStackPanel;

            buyNowButton.Click += ButtonCartBuyNow_Click;
            deleteButton.Click += ButtonCartDelete_Click;
            // Add the items to the stack panel
            stackPanel.Children.Add(innerBorder);
            stackPanel.Children.Add(quantityLabel);
            stackPanel.Children.Add(totalLabel);
            stackPanel.Children.Add(buyNowButton);
            stackPanel.Children.Add(deleteButton);

            // Add the outer border and stack panel to the main grid
            cartGrid.Children.Add(outerBorder);
            cartGrid.Children.Add(stackPanel);

            // Finally import created Items to Display
            stackPanelCart.Children.Add(cartGrid);

        }

       

        // Button Clicks
        private void ButtonCartBuyNow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is StackPanel stackpanel && stackpanel.Parent is Grid grid)
            {
                try
                {
                    string cartID =  grid.Name;
                    string sqlRead = $"SELECT * FROM Cart WHERE Cart_Id = '{cartID}'";
                    SqlCommand cmdRead = new SqlCommand(sqlRead, con.GetDBconnetion());
                    SqlDataReader reader = cmdRead.ExecuteReader();

                    if (reader.Read())
                    {
                        string itemID = Convert.ToString(reader["Item_ID"]);
                        int qty = Convert.ToInt32(reader["Cart_Qty"]);

                        PaymentUC cartPay = new PaymentUC();
                        cartPay.setCartID(cartID);
                        cartPay.setItem(itemID, qty);

                        if (Window.GetWindow(this) is ShoppingWindow shoppingWindow)
                        {
                            shoppingWindow.ContentDisplay.Children.Clear();
                            shoppingWindow.ContentDisplay.Children.Add(cartPay);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonCartDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is StackPanel stackpanel && stackpanel.Parent is Grid grid)
            {
                try
                {
                    string sqlDeleteCart = $"DELETE FROM Cart WHERE Cart_ID = '{grid.Name}'";

                    // Delete cart from Database
                    SqlCommand cmdDeleteCart = new SqlCommand(sqlDeleteCart, con.GetDBconnetion());
                    cmdDeleteCart.ExecuteNonQuery();

                    // Delete cart from UI
                    stackPanelCart.Children.Remove(grid);
                }
                catch(Exception ex)
                { 
                    MessageBox.Show("Error while removing cart from database\n\n"+ex.Message);
                }
            }
        }
    }
}
