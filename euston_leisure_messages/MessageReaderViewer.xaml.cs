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
    /// Interaction logic for MessageReaderViewer.xaml
    /// </summary>
    public partial class MessageReaderViewer : Page
    {

        public MessageReaderViewer(Message m)
        {
            InitializeComponent();
            loadContent(m);
        }

        /// used to load the data inside the page 
        /// <param name="m">Message to be viewed</param>
        public void loadContent(Message m)
        {
            //shows SIR email data
            if (m is SIR)
            {
                SIR message = (SIR)m;
                messageLabel.Content += "Sender: ";
                messageLabel.Content += message.sender + Environment.NewLine;
                messageLabel.Content += "Date reported: ";
                messageLabel.Content += message.dateReported.ToString("dd/MM/yy") + Environment.NewLine;
                messageLabel.Content += "Sort code: ";
                messageLabel.Content += message.centCode + Environment.NewLine;
                messageLabel.Content += "Nature of incident: ";
                messageLabel.Content += message.natureOfIncident + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;
                messageBodyTb.Text = message.messageBody;

            }
            //shows email data
            else if (m is Email)
            {
                Email message = (Email)m;
                messageLabel.Content += "Sender: ";
                messageLabel.Content += message.sender + Environment.NewLine;
                messageLabel.Content += "Subject: ";
                messageLabel.Content += message.subject + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += " " + Environment.NewLine;
                messageLabel.Content += "Message: " + Environment.NewLine;
                messageBodyTb.Text = message.messageBody;
            }
            //shows SMS email data
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

                messageBodyTb.Text = getAbbreviations(message.messageBody);
            }
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

                messageBodyTb.Text = getAbbreviations(message.messageBody);
            }
        }

        //replaces any textspeak words used inside the message
        /// <param name="body">Message body to be checked for textspeak abbreviations</param>
        public String getAbbreviations(string body)
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
