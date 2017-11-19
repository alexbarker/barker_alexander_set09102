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

namespace euston_leisure_messages.Views
{
    /// <summary>
    /// Interaction logic for AddSMS.xaml
    /// </summary>
    public partial class AddSMS : Window
    {
        public AddSMS()
        {
            InitializeComponent();
        }

        private void Close_SMS_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_SMS_Button_Click(object sender, RoutedEventArgs e)
        {



            this.Close();
        }
    }
}
