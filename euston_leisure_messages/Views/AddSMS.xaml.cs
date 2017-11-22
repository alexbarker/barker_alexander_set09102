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
    /// SET09102 2017-8 TR1 001 - Software Engineering
    /// Euston Leisure Message System
    /// Version 0.5.0
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 22th November 2017
    /// </summary>
    /// <summary>
    /// AddSMS.xaml.cs - This class is responsible for gathering and validating user inputs for SMS message.
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
            if (ValidateSMS())
            {
                string currentID = id.Text; // Gets id from textbox.
                // Forces message ID to be 9 characters.
                if (currentID.Length < 9)
                {
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;

                }
                currentID = "S" + currentID; // Makes sure S is added to the message ID for SMS type.

                // Creates SMS objects to be used later.
                SMS sms = new SMS(currentID + " " + phoneNoTextbox.Text + " " + messageTextbox.Text);
                MessageHolder.currentSMSID++;
                MessageHolder.AddMessage(currentID, sms);

                // Confirmation message for user.
                MessageBox.Show("SMS Added!\n" + "Message ID: " + currentID + "\nPhone No.: " + phoneNoTextbox.Text + "\nMessage: " + messageTextbox.Text);
            }
        }

        // Validates user input via textboxes.
        public bool ValidateSMS()
        {
            bool checker = true;

            int parsedValue;
            if (!int.TryParse(id.Text, out parsedValue))
            {
                MessageBox.Show("Please enter numbers only!");
                checker = false;
            }
            if (id.Text.Length > 9 || String.IsNullOrEmpty(id.Text))
            {
                MessageBox.Show("Invalid ID!");
                checker = false;
            }

            string phoneNo = phoneNoTextbox.Text.Replace(" ", "");
            phoneNo = phoneNoTextbox.Text.Replace("+", "");
            long n;
            bool isNumeric = long.TryParse(phoneNo, out n);

            if (!isNumeric || string.IsNullOrEmpty(phoneNo))
            {
                MessageBox.Show("Please enter a phone number!");
                checker = false;
            }
            else if (phoneNo.Length != 12)
            {
                MessageBox.Show("Please enter a valid phone number!");
                checker = false;
            }
            if (messageTextbox.Text.Length >= 140)
            {
                MessageBox.Show("Exceeded 140 characters!");
                checker = false;
            }
            else if (string.IsNullOrEmpty(messageTextbox.Text))
            {
                MessageBox.Show("Please enter a message!");
                checker = false;
            }

            return checker;
        }
    }
}
