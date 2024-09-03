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
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class MsgWindow : Window
    {
        public MsgWindow()
        {
            InitializeComponent();
            //textBoxMsg.Text = message;
        }
       
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Button Clicks
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
