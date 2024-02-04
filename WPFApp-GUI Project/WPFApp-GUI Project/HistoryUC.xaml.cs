using System;
using System.Collections;
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
    /// Interaction logic for HistoryUC.xaml
    /// </summary>
    public partial class HistoryUC : UserControl
    {
        CreateDBconnection con = new CreateDBconnection();
        private string loggeUserID;

        public HistoryUC()
        {
            InitializeComponent();
            loggeUserID = ShoppingWindow.getLoggedID();
            checkHistory();
        }

        // Read history from Database relavent to suxtomer ID
        private void checkHistory()
        {
            string sqlCheckHistory = $"SELECT * FROM Orrder WHERE Cus_ID = '{loggeUserID}' ORDER BY Order_Date DESC";


            try
            {
                SqlCommand cmdCheckHistory = new SqlCommand(sqlCheckHistory, con.GetDBconnetion());
                SqlDataReader reader = cmdCheckHistory.ExecuteReader();

                while (reader.Read())
                {
                    string itemID = reader["Item_ID"].ToString();
                    int qty = Convert.ToInt32(reader["Qty"]);
                    int bill = Convert.ToInt32(reader["Bill"]);
                    string oDate = reader["Order_Date"].ToString();
                    string oAddress = reader["Order_Address"].ToString();

                    createHistory(itemID, qty, bill, oDate, oAddress);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Error occur while Read history from Database\n\n"+ex.Message);            
            }
        }

        // Create the list of history to Display
        private void createHistory(string itemID, int qty, int price, string date, string address)
        {
            try
            {
                string itemName = itemID;

                // Create the main grid
                Grid itemGrid = new Grid
                {
                    Height = 136
                };

                // Create the outer border
                Border outerBorder = new Border
                {
                    BorderThickness = new Thickness(9),
                    CornerRadius = new CornerRadius(45),
                    Margin = new Thickness(80, 18, 80, -2),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(95, 237, 203)),
                    Height = 110
                };

                // Create the stack panel
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(82, 18, 81, 0)
                };

                // Create the round image
                Border imageBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(166, 166, 166)),
                    BorderThickness = new Thickness(3.4),
                    Width = 75,
                    Height = 74,
                    CornerRadius = new CornerRadius(30),
                    Margin = new Thickness(20, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemName}.jpg", UriKind.Absolute)),
                };
                imageBorder.Background = imageBrush;

                // Create Quantity label
                Label quantityLabel = new Label
                {
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Content = $"Quantity {qty}",
                    Height = 38,
                    Margin = new Thickness(40, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 17,
                    Width = 114,
                    FontFamily = new FontFamily("Calibri")
                };

                // Create Price label
                Label priceLabel = new Label
                {
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Content = $"LKR {price}",
                    Height = 38,
                    Width = 114,
                    Margin = new(30, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 17,
                    FontFamily = new FontFamily("Calibri")

                };

                // Create Date label
                TextBlock dateLabel = new TextBlock
                {
                    TextAlignment = TextAlignment.Right,
                    Text = $"Date:  {date}",
                    TextWrapping = TextWrapping.Wrap,
                    Height = 38,
                    Width = 128,
                    Margin = new(20, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 17,
                    FontFamily = new FontFamily("Calibri")
                };

                // Create addressTextBox
                TextBox addressTextBox = new TextBox
                {
                    SelectionBrush = null,
                    Focusable = false,
                    IsHitTestVisible = false,
                    Background = null,
                    BorderBrush = null,
                    IsReadOnly = true,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Left,
                    Height = 82,
                    Margin = new Thickness(30, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 17,
                    Width = 177,
                    FontFamily = new FontFamily("Calibri"),
                    Text = $"{address}",
                    TextWrapping = TextWrapping.Wrap
                };

                // Add the controls to the stack panel
                stackPanel.Children.Add(imageBorder);
                stackPanel.Children.Add(quantityLabel);
                stackPanel.Children.Add(priceLabel);
                stackPanel.Children.Add(dateLabel);
                stackPanel.Children.Add(addressTextBox);
                itemGrid.Children.Add(outerBorder);
                itemGrid.Children.Add(stackPanel);

                // Finally add the created grid to the stactpanel as a list item
                stackPanelHistory.Children.Add(itemGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while creating Histroy items\n\n"+ex.Message);
            }
        }
    }
}
