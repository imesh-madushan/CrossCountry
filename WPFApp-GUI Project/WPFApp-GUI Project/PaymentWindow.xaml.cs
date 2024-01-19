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
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : UserControl
    {
        string itemName;
        int itemQtyLeft = 22; 
        public PaymentWindow()
        {
            InitializeComponent();
        }

        public void getItemID(string itemFromGrid)
        { 
            itemName = itemFromGrid;
            labelQtyLeft.Content = itemQtyLeft+" Left";
            borderBuyItem.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/WPFApp-GUI Project;component/products/{itemName}.jpg", UriKind.Absolute)));
        }

        //Button Clicks
        private void ButtonPay_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(1.5);
            }
        }

        private void ButtonPay_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 95, 237, 203));
                button.BorderThickness = new Thickness(1.8);
            }
        }


        //Button Enter and Leaves
    }
}
