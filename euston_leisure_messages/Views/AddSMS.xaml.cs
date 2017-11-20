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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            if (validateInput())
            {
                string currentID = id.Text;
                if (currentID.Length < 9)
                {
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;

                }
                currentID = "S" + currentID;

                SMS sms = new SMS(currentID + " " + phoneNoTextbox.Text + " " + messageTextbox.Text);
                MessageHolder.currentSMSID++;
                MessageHolder.addMessage(currentID, sms);
            }
        }

        //validates the textboxes to ensure format meets the specification described in the
        //initial documentation
        public bool validateInput()
        {
            bool canAdd = true;
            string phoneNo = phoneNoTextbox.Text.Replace(" ", "");
            phoneNo = phoneNoTextbox.Text.Replace("+", "");
            long n;
            bool isNumeric = long.TryParse(phoneNo, out n);

            if (!isNumeric || string.IsNullOrEmpty(phoneNo))
            {
                MessageBox.Show("Please enter a valid number(International format).");
                canAdd = false;
            }
            else if (phoneNo.Length != 12)
            {
                MessageBox.Show("Please enter a valid number(International format) .");
                canAdd = false;
            }
            if (messageTextbox.Text.Length >= 140)
            {
                MessageBox.Show("140 Character maximum for message text.");
                canAdd = false;
            }
            else if (string.IsNullOrEmpty(messageTextbox.Text))
            {
                MessageBox.Show("Please enter a message ");
                canAdd = false;
            }

            return canAdd;
        }
    }
}
