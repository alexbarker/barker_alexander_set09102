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
    /// Version 0.4.5
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// AddEmail.xaml.cs - 
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
            if (validateInput())
            {
                string currentID = id.Text; //unique id for email
                if (currentID.Length < 9)
                {
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length)); //padding for subject as spec states all subjects are to be 20 characters
                    currentID = zeros + currentID;

                }
                currentID = "E" + currentID;

                Email email = new Email(currentID + " " + emailTextbox.Text + " " + subjectTextbox.Text + " " + messageTextbox.Text);
                MessageHolder.currentEmailID++;
                MessageHolder.addMessage(currentID, email);
            }

        }

        //validates input according to initial specification before sending
        private bool validateInput()
        {
            string pattern = @"[!#$%&'\\*\\+\\-\\/\\=\\?\\^\\_`\\{\\|\\}\\~\\+a-zA-Z0-9\\.]+@.*?[a-zA-Z0-9\\.]+";
            Regex re = new Regex(pattern);
            Match m = Regex.Match(emailTextbox.Text, pattern);
            bool canAdd = true;

            int parsedValue;
            if (!int.TryParse(id.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                canAdd = false;
            }
            if (id.Text.Length > 9 || String.IsNullOrEmpty(id.Text))
            {
                MessageBox.Show("Invalid id");
                canAdd = false;
            }

            if (!m.Success)
            {
                MessageBox.Show("Invalid email address ");
                canAdd = false;
            }

            if (subjectTextbox.Text.Length >= 20)
            {
                MessageBox.Show("Subject must be less than 20 characters  ");
                canAdd = false;
            }
            else if (String.IsNullOrEmpty(subjectTextbox.Text))
            {
                MessageBox.Show("Please enter a subject ");
                canAdd = false;
            }
            else if (subjectTextbox.Text.Length < 20)
            {
                subjectTextbox.Text += String.Concat(Enumerable.Repeat(" ", 20 - subjectTextbox.Text.Length));
            }
            if (messageTextbox.Text.Length >= 1028)
            {
                MessageBox.Show("Message too long (1028 characters max) ");
                canAdd = false;
            }
            else if (string.IsNullOrEmpty(messageTextbox.Text))
            {
                MessageBox.Show("Please enter a message  ");
                canAdd = false;
            }
            return canAdd;
        }
    }
}
