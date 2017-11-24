using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    /// Tweet.cs - This class is responsible for validating and preparing Tweet messages.
    /// </summary>

    public class Tweet : Message
    {

        /// <summary>
        /// Tweet validation, adds new Tweet.
        /// </summary>
        /// <param name="messageIn">Message body.</param>
        public Tweet(string messageIn)
        {
            Regex re = new Regex(@"(T\d{9}) (@.*?) (.+)");
            Match m = re.Match(messageIn);
            if (m.Success)
            {
                if (ValidateTweet(m.Groups[2].ToString(), m.Groups[3].ToString()))
                {
                    this.messageID = m.Groups[1].ToString();
                    this.sender = m.Groups[2].ToString();
                    this.messageBody = m.Groups[3].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Tweet!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid Tweet!");
                return;
            }

            // Search the message body for #.
            string[] hashtags = this.messageBody.Split(' ');
            foreach (string h in hashtags)
            {
                if (h.StartsWith("#") || h.StartsWith("@"))
                {
                    string tag = h;
                    if (MessageHolder.mentions.ContainsKey(tag)) 
                    {
                        if (!MessageHolder.mentions[tag].Contains(this.messageID))
                        {
                            MessageHolder.mentions[tag].Add(this.messageID); // Adds the mention to a list.
                        }
                    }
                    else
                    {
                        MessageHolder.mentions.Add(tag, new List<String>());
                        MessageHolder.mentions[tag].Add(this.messageID);
                    }
                }
            }
        }

        public bool ValidateTweet(string se, string mb)
        {
            if (se.Length <= 15 && mb.Length <= 140)
            {
                if (se.StartsWith("@"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override MessageReader returnData()
        {
            return new MessageReader(this.messageID, this.sender + " " + this.messageBody);
        }
    }
}
