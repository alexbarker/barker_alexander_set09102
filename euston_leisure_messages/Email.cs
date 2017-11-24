using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace euston_leisure_messages
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
    /// Email.cs - This class is used to process, store and validate Email messages.
    /// </summary>

    public class Email : Message
    {
        public string Subject { get; set; }
        /// Creates and validates Email messages.
        /// <param name="currentMessage">Creates an email string.</param>
        public Email(string currentMessage)
        {
            currentMessage = currentMessage.Replace(Environment.NewLine, " ");
            // 0 - Whole message. 
            // 1 - Message ID. 
            // 2 - Email address.
            // 3 - Subject. 
            // 4 - Message body.
            Regex regex = new Regex(@"(E\d{9}) ([!#$%&'\\*\\+\\-\\/\\=\\?\\^\\_`\\{\\|\\}\\~\\+a-zA-Z0-9\\.]+@.*?[a-zA-Z0-9\\.]+) (.{20}) (.+)");
            Match match = regex.Match(currentMessage);
            if (match.Success)
            {
                if (ValidateEmail(match.Groups[2].ToString(), match.Groups[3].ToString(), match.Groups[4].ToString()))
                {
                    this.messageID = match.Groups[1].ToString();
                    this.sender = match.Groups[2].ToString();
                    this.Subject = match.Groups[3].ToString();
                    this.messageBody = match.Groups[4].ToString();
                    bool hasChanged = true;

                    while (hasChanged)
                    {
                        // Detects URLs.
                        regex = new Regex(@"\S+\.\S+");
                        match = regex.Match(this.messageBody);

                        if (match.Success)
                        {
                            foreach (Match g in match.Groups)
                            {
                                // Checks URL list for duplicates.
                                if (MessageHolder.quarantined.ContainsKey(g.ToString()))
                                {
                                    MessageHolder.quarantined[g.ToString()] = (Convert.ToInt32(MessageHolder.quarantined[g.ToString()]) + 1).ToString();
                                }
                                else
                                {
                                    MessageHolder.quarantined.Add(g.ToString(), "1");
                                }
                                MessageHolder.UpdateURLs();
                                this.messageBody = this.messageBody.Replace(g.ToString(), "<<Quarantined Link>>");
                            }
                        }
                        else
                        {
                            hasChanged = false;
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid input");
            }
        }

        /// Makes sure email is in the correct format
        /// <param name="sender">Email address.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="body">Message text.</param>
        public bool ValidateEmail(string sender, string subject, string body)
        {
            if (sender.Length > 0 && subject.Length == 20 && body.Length < 1029)
            {
                return true;
            }
            return false;
        }

        /// Formats output for Json file.
        public override MessageReader returnData()
        {
            return new MessageReader(this.messageID, this.sender + " " + this.Subject + " " + this.messageBody);
        }
    }
}
