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
    /// Version 0.4.5
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// Tweet.cs - 
    /// </summary>

    public class Tweet : Message
    {

        /// <summary>
        /// validates and creates a new tweet message
        /// </summary>
        /// <param name="messageIn"></param>
        public Tweet(string messageIn)
        {
            Regex re = new Regex(@"(T\d{9}) (@.*?) (.+)");
            Match m = re.Match(messageIn);
            if (m.Success)
            {
                if (validateInput(m.Groups[2].ToString(), m.Groups[3].ToString()))
                {
                    this.messageID = m.Groups[1].ToString();
                    this.sender = m.Groups[2].ToString();
                    this.messageBody = m.Groups[3].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Tweet");
                    //throw new ArgumentException("Invalid tweet message");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid Tweet");
                //throw new ArgumentException("Invalid Tweet data");
                //this.Close();
                //Views.AddTweet form = new Views.AddTweet();
                //form.ShowDialog();
                return;
            }
            //find all the hashtags in the message body
            string[] hashtags = this.messageBody.Split(' ');
            foreach (string h in hashtags)
            {
                if (h.StartsWith("#") || h.StartsWith("@"))
                {
                    string tag = h;
                    if (MessageHolder.mentions.ContainsKey(tag)) //check if its already used
                    {
                        if (!MessageHolder.mentions[tag].Contains(this.messageID))
                        {
                            MessageHolder.mentions[tag].Add(this.messageID); //add this meessage id to the list if its been used 
                        }
                    }
                    else
                    {
                        MessageHolder.mentions.Add(tag, new List<String>()); //create new dictionary item for hashtag if not found
                        MessageHolder.mentions[tag].Add(this.messageID);
                    }
                }
            }
        }

        public bool validateInput(string se, string mb)
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
