using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace euston_leisure_messages.Views
{
    /// <summary>
    /// SET09102 2017-8 TR1 001 - Software Engineering
    /// Euston Leisure Message System
    /// Version 1.0.0
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 22nd November 2017
    /// </summary>
    /// <summary>
    /// AddSIR.xaml.cs - This class is responsible for gathering and vaildating user inputs for SIR email.
    /// </summary>

    public partial class AddSIR : Window
    {
        public AddSIR()
        {
            InitializeComponent();
        }

        private void Close_SIREmail_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_SIREmail_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSIREmail())
            {
                string currentID = id.Text; // Gets id from textbox. 
                if (currentID.Length < 9)
                {
                    // Forces message ID to be 9 characters.
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;
                }

                currentID = "E" + currentID; // Makes sure E is added to the message ID for email type.

                // Collects incident type from drop down menu.
                ComboBoxItem cmb = (ComboBoxItem)incidentTypeCombo.SelectedItem;
                string subject = "SIR " + System.DateTime.Now.ToString("dd/MM/yy");
                string messageBody = "Cent Code: " + centerCode1.Text + "-" + centerCode2.Text + "-" + centerCode3.Text + " " + "Nature of Incident: " + cmb.Content + " " + messageTextbox.Text;

                subject += String.Concat(Enumerable.Repeat(" ", 20 - subject.Length));

                // Creates SIR email objects to be used later.
                SIR sir = new SIR(currentID + " " + emailTextbox.Text + " " + subject + messageBody);
                MessageHolder.currentEmailID++;
                MessageHolder.AddMessage(currentID, sir);

                // Confirmation message for user.
                MessageBox.Show("SIR Email Added!\n" + "Message ID: " + currentID + "\nEmail: " + emailTextbox.Text + "\nSubject: " + subject + "\nIncident: " + cmb.Content + "\nMessage: " + messageTextbox.Text);
            }
        }

        // Validates user input via textboxes.
        private bool ValidateSIREmail()
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

            int x, y, z;

            bool centreC = int.TryParse(centerCode1.Text, out x);
            if (centreC)
            {

                centreC = int.TryParse(centerCode2.Text, out y);
                if (centreC)
                {
                    centreC = int.TryParse(centerCode3.Text, out z);
                }
            }
            if (!centreC)
            {
                MessageBox.Show("Enter a valid centre code!");
            }

            if (centerCode1.Text.Length != 2 || centerCode2.Text.Length != 3 || centerCode3.Text.Length != 2)
            {
                checker = false;
            }

            ComboBoxItem comboBox = (ComboBoxItem)incidentTypeCombo.SelectedItem;

            if (comboBox == null)
            {
                checker = false;
                MessageBox.Show("Please select an incident type!");
            }
            if (String.IsNullOrEmpty(messageTextbox.Text))
            {
                MessageBox.Show("Please enter a message!");
            }
            return checker;

        }
    }
}
