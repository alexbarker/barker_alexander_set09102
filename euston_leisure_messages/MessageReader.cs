using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
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
    /// MessageReader.cs - This class get the header and body objects from Json file.
    /// </summary>

    public class MessageReader
    {
        public string header { get; set; }
        public string body { get; set; }

        public MessageReader()
        {

        }

        /// Used to read from the Json file.
        /// <param name="h"> Message header.</param>
        /// <param name="b"> Message body.</param>
        public MessageReader(string h, string b)
        {
            this.header = h;
            this.body = b;
        }

        /// Selector based on message type.
        /// <param name="messageIn"> Message from user.</param>
        public Message ConvertMessage(String messageIn)
        {
            if (messageIn.StartsWith("E"))
            {
                if (GetEmailType(messageIn) == "SIR")
                {
                    return new SIR(messageIn);
                }
                else
                {
                    return new Email(messageIn);
                }
            }
            else if (messageIn.StartsWith("S"))
            {
                return new SMS(messageIn);
            }
            else if (messageIn.StartsWith("T"))
            {
                return new Tweet(messageIn);
            }
            else
            {
                return null;
            }
        }

        /// Makes message available after conversion.
        public Message LoadMessage()
        {
            Message msg = ConvertMessage(this.header + " " + this.body);
            return msg;
        }

        /// Seperates normal Email from SIR.
        /// <param name="messageIn"> Message body.</param>
        private string GetEmailType(string messageIn)
        {
            string messageID = messageIn.Substring(0, 10);
            messageIn = messageIn.Substring(11);
            string sender = messageIn.Split(' ')[0];
            string subject = messageIn.Substring(sender.Length + 1, 20);
            int size = subject.Length + sender.Length;
            if (subject.StartsWith("SIR"))
            {
                return "SIR";
            }
            else
            {
                return "Email";
            }
        }
    }
}
