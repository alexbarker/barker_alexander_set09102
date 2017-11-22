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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace euston_leisure_messages
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
    /// MessageReaderViewer.cs - This class outputs the message data to a viewer window.
    /// </summary>
    
    public partial class MessageReaderViewer : Page
    {

        public MessageReaderViewer(Message m)
        {
            InitializeComponent();
            LoadContent(m);
        }

        /// Displays data to the window.
        /// <param name="m"> Message to be displayed.</param>
        public void LoadContent(Message m)
        {
            // SIR Email.
            if (m is SIR)
            {
                SIR message = (SIR)m;
                messageLabel.Content += "Sender: ";
                string s = message.sender;
                string[] words = s.Split(' ');
                messageLabel.Content += words[0] + Environment.NewLine;
                messageLabel.Content += "Date Reported: ";
                messageLabel.Content += message.dateReported.Substring(0, 8) + Environment.NewLine;
                messageLabel.Content += "Centre Code: ";
                messageLabel.Content += message.centCode + Environment.NewLine;
                messageLabel.Content += "Incident Type: ";
                messageLabel.Content += message.natureOfIncident + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;
                messageBodyTb.Text = message.messageBody;

            }
            // Normal Email.
            else if (m is Email)
            {
                Email message = (Email)m;
                messageLabel.Content += "Sender: ";
                messageLabel.Content += message.sender + Environment.NewLine;
                messageLabel.Content += "Subject: ";
                messageLabel.Content += message.Subject + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;
                messageBodyTb.Text = message.messageBody;
            }
            // SMS.
            else if (m is SMS)
            {
                SMS message = (SMS)m;
                messageLabel.Content += "Sender: ";
                messageLabel.Content += message.sender + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;

                messageBodyTb.Text = GetAbbreviations(message.messageBody);
            }
            // Tweet.
            else if (m is Tweet)
            {
                Tweet message = (Tweet)m;
                messageLabel.Content += "Sender: ";
                messageLabel.Content += message.sender + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;

                messageBodyTb.Text = GetAbbreviations(message.messageBody);
            }
        }

        // Adds the full length text next to the textspeak abbreviation.
        /// <param name="body"> The message to be scanned for abbreviations.</param>
        public String GetAbbreviations(string body)
        {
            string[] message = body.Split(' ');
            List<int> toChange = new List<int>();
            for (int i = 0; i < message.Length; i++)
            {
                if (MessageHolder.textspeak.ContainsKey(message[i]))
                {
                    toChange.Add(i);
                }
            }
            foreach (int c in toChange)
            {
                message[c] = message[c] + "<<" + MessageHolder.textspeak[message[c]] + ">>";
            }
            return String.Join(" ", message);
        }
    }
}
