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
    /// Version 0.4.4
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// Email.cs - 
    /// </summary>

    public class Email : Message
    {
        public string subject { get; set; }
        /// constructor for creating and validating email messages
        /// <param name="messageIn">raw string message</param>
        public Email(string messageIn)
        {
            messageIn = messageIn.Replace(Environment.NewLine, " ");
            //reg ex returns 0-full message 1-message id 2-email address of sender 3-subject 4-message body
            Regex regex = new Regex(@"(E\d{9}) ([!#$%&'\\*\\+\\-\\/\\=\\?\\^\\_`\\{\\|\\}\\~\\+a-zA-Z0-9\\.]+@.*?[a-zA-Z0-9\\.]+) (.{20}) (.+)");
            Match match = regex.Match(messageIn);
            if (match.Success)
            {
                if (validateInput(match.Groups[2].ToString(), match.Groups[3].ToString(), match.Groups[4].ToString()))
                {
                    this.messageID = match.Groups[1].ToString();
                    //this.seen = false;
                    this.sender = match.Groups[2].ToString();
                    this.subject = match.Groups[3].ToString();
                    this.messageBody = match.Groups[4].ToString();
                    bool hasChanged = true;

                    while (hasChanged)
                    {
                        //checks for website URL's in message body
                        regex = new Regex(@"\S+\.\S+");
                        match = regex.Match(this.messageBody);


                        if (match.Success)
                        {
                            foreach (Match g in match.Groups)
                            {
                                //checks if URL already exists in the quarantine list
                                if (MessageHolder.quarantined.ContainsKey(g.ToString()))
                                {
                                    MessageHolder.quarantined[g.ToString()] = (Convert.ToInt32(MessageHolder.quarantined[g.ToString()]) + 1).ToString();
                                }
                                else
                                {
                                    MessageHolder.quarantined.Add(g.ToString(), "1");
                                }
                                MessageHolder.updateQuarantinedItems();
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

        /// method used to make sure emails meet the standards in the requirement specification
        /// <param name="sender">email address of sender</param>
        /// <param name="subject">subject of the email.</param>
        /// <param name="body">message body of the email.</param>
        public bool validateInput(string sender, string subject, string body)
        {
            if (sender.Length > 0 && subject.Length == 20 && body.Length < 1029)
            {
                return true;
            }
            return false;
        }
        /// method used concatenate the string for JSON output
        public override MessageReader returnData()
        {
            return new MessageReader(this.messageID, this.sender + " " + this.subject + " " + this.messageBody);
        }
    }
}
