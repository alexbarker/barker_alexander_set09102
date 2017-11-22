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
using System.Text.RegularExpressions;

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
    /// AddEmail.xaml.cs - This class is responsible for collecting and storing user inputs for email type messages.
    /// </summary>
    
    public partial class AddEmail : Window
    {
        public AddEmail()
        {
            InitializeComponent();
        }

        private void Close_Email_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Email_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmail())
            {
                string currentID = id.Text; // Gets id from textbox.  
                if (currentID.Length < 9)
                {
                    // Forces message ID to be 9 characters.
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;
                }

                currentID = "E" + currentID; // Makes sure E is added to the message ID for email type.

                // Creates email objects to be used later.
                Email email = new Email(currentID + " " + emailTextbox.Text + " " + subjectTextbox.Text + " " + messageTextbox.Text);
                MessageHolder.currentEmailID++;
                MessageHolder.AddMessage(currentID, email);

                // Confirmation message for user.
                MessageBox.Show("Email Added!\n" + "Message ID: " + currentID + "\nEmail: " + emailTextbox.Text + "\nSubject: " + subjectTextbox.Text + "\nMessage: " + messageTextbox.Text);
            }
        }

        // Validates user input via textboxes.
        private bool ValidateEmail()
        {
            string pattern = @"[!#$%&'\\*\\+\\-\\/\\=\\?\\^\\_`\\{\\|\\}\\~\\+a-zA-Z0-9\\.]+@.*?[a-zA-Z0-9\\.]+";
            Regex re = new Regex(pattern);
            Match m = Regex.Match(emailTextbox.Text, pattern);
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

            if (!m.Success)
            {
                MessageBox.Show("Invalid email address!");
                checker = false;
            }
            if (subjectTextbox.Text.Length >= 20)
            {
                MessageBox.Show("Subject must be less than 20 characters!");
                checker = false;
            }
            else if (String.IsNullOrEmpty(subjectTextbox.Text))
            {
                MessageBox.Show("Please enter a subject!");
                checker = false;
            }
            else if (subjectTextbox.Text.Length < 20)
            {
                subjectTextbox.Text += String.Concat(Enumerable.Repeat(" ", 20 - subjectTextbox.Text.Length));
            }
            if (messageTextbox.Text.Length >= 1028)
            {
                MessageBox.Show("Message too long! (1028 maximum)");
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
