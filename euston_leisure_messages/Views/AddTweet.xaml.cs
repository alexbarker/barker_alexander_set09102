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
    /// AddTweet.xaml.cs - This class is responsible for gathering and validating Tweet messages.
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
            if (ValidateTweet())
            {
                string currentID = id.Text; // Gets id from textbox. 
                if (currentID.Length < 9)
                {
                    // Forces message ID to be 9 characters.
                    string zeros = String.Concat(Enumerable.Repeat("0", 9 - currentID.Length));
                    currentID = zeros + currentID;

                }
                currentID = "T" + currentID; // Makes sure E is added to the message ID for email type.

                // Creates Tweet objects to be used later.
                Tweet tweet = new Tweet(currentID + " " + twitterHandle.Text + " " + twitterMessage.Text);
                MessageHolder.currentTwitterID++;
                MessageHolder.AddMessage(currentID, tweet);

                // Confirmation message for user.
                MessageBox.Show("Tweet Added!\n" + "Message ID: " + currentID + "\nHandle: " + twitterHandle.Text + "\nMessage: " + twitterMessage.Text);
            }
        }

        // Validates user input via textboxes.
        private bool ValidateTweet()
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

            // Makes sure Twitter handles start with "@"
            if (twitterHandle.Text.StartsWith("@")) 
            {
                if (twitterHandle.Text.Length > 15 || String.IsNullOrEmpty(twitterHandle.Text))
                {
                    MessageBox.Show("Enter a valid Twitter handle! (15 characters)");
                    checker = false;
                }
            }
            else
            {
                MessageBox.Show("Twitter handles must start with @!");
                checker = false;
            }
            if (twitterMessage.Text.Length > 140 || String.IsNullOrEmpty(twitterMessage.Text))
            {
                MessageBox.Show("140 character limit breached!");
                checker = false;
            }
            return checker;

        }
    }
}

