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
    /// Version 0.4.3
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// AddTweet.xaml.cs - 
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

