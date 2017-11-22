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
    /// Version 0.4.5
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// AddSIR.xaml.cs - 
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
            if (validateInput())
            {
                string currentID = id.Text;
                if (currentID.Length < 9)
                {
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;

                }
                currentID = "E" + currentID;

                ComboBoxItem cmb = (ComboBoxItem)incidentTypeCombo.SelectedItem;
                string subject = "SIR " + System.DateTime.Now.ToString("dd/MM/yy");
                string messageBody = "Cent Code: " + centerCode1.Text + "-" + centerCode2.Text + "-" + centerCode3.Text + " " + "Nature of Incident: " + cmb.Content + " " + messageTextbox.Text;

                subject += String.Concat(Enumerable.Repeat(" ", 20 - subject.Length));

                SIR sir = new SIR(currentID + " " + emailTextbox.Text + " " + subject + messageBody);
                MessageHolder.currentEmailID++;
                MessageHolder.addMessage(currentID, sir);

                MessageBox.Show(currentID + " " + emailTextbox.Text + " " + subject + messageBody);
            }
        }

        /// <summary>
        /// ensures that input meets standards described in initial docs
        /// </summary>
        /// <returns></returns>
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
                //errorLbl.Content += "Message cant be more than 140 characters " + Environment.NewLine;
                canAdd = false;
            }

            if (!m.Success)
            {
                MessageBox.Show("Invalid email address ");
                canAdd = false;
            }

            int cc1, cc2, cc3;

            bool ty = int.TryParse(centerCode1.Text, out cc1);
            if (ty)
            {

                ty = int.TryParse(centerCode2.Text, out cc2);
                if (ty)
                {
                    ty = int.TryParse(centerCode3.Text, out cc3);
                }
            }
            if (!ty)
            {
                MessageBox.Show("Enter a valid centre code ");
            }

            if (centerCode1.Text.Length != 2 || centerCode2.Text.Length != 3 || centerCode3.Text.Length != 2)
            {
                canAdd = false;
            }

            ComboBoxItem cmb = (ComboBoxItem)incidentTypeCombo.SelectedItem;

            if (cmb == null)
            {
                canAdd = false;
                MessageBox.Show("Please select an incident type ");
            }
            if (String.IsNullOrEmpty(messageTextbox.Text))
            {
                MessageBox.Show("Please provide details of Incident ");
            }
            return canAdd;

        }
    }
}
