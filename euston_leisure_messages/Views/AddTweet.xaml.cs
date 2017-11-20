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
    /// Interaction logic for AddTweet.xaml
    /// </summary>
    public partial class AddTweet : Window
    {
        public AddTweet()
        {
            InitializeComponent();
        }

        private void Close_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {
            if (validateInput())
            {
                string currentID = id.Text;
                if (currentID.Length < 9)
                {
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;

                }
                currentID = "T" + currentID;

                Tweet tweet = new Tweet(currentID + " " + twitterHandle.Text + " " + twitterMessage.Text);
                MessageHolder.currentTwitterID++;
                MessageHolder.addMessage(currentID, tweet);

            }
        }

        //validates the textboxes to ensure format meets the specification described in the
        //initial documentation
        private bool validateInput()
        {           
            bool canAdd = true;
            if (twitterHandle.Text.StartsWith("@")) //all twitter handles start with "@"
            {
                if (twitterHandle.Text.Length > 15 || String.IsNullOrEmpty(twitterHandle.Text))
                {
                    MessageBox.Show("Twitter handle be between 0 and 15 characters .");
                    //errorLbl.Content += "Twitter handle be between 0 and 15 characters " + Environment.NewLine;
                    canAdd = false;
                }
            }
            else
            {
                MessageBox.Show("Twitter handles must start with @ .");
                //errorLbl.Content += "Twitter handles must start with @ " + Environment.NewLine;
                canAdd = false;
            }
            if (twitterMessage.Text.Length > 140 || String.IsNullOrEmpty(twitterMessage.Text))
            {
                MessageBox.Show("Message cant be more than 140 characters");
                //errorLbl.Content += "Message cant be more than 140 characters " + Environment.NewLine;
                canAdd = false;
            }
            return canAdd;

        }
    }
}

