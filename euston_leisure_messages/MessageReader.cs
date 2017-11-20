﻿using System;
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
    public class MessageReader
    {
        public string header { get; set; }
        public string body { get; set; }

        public MessageReader()
        {

        }

        /// MessageProcessor is used to write out to the JSON file so the varables must match the JSON format
        /// <param name="h">message header</param>
        /// <param name="b">message body</param>
        public MessageReader(string h, string b)
        {
            this.header = h;
            this.body = b;
        }

        /// selects the correct message type for the message being passed in(in text format)
        /// <param name="messageIn">message string</param>
        public Message convertMessage(String messageIn)
        {
            if (messageIn.StartsWith("E"))
            {
                if (getEmailType(messageIn) == "SIR")
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
                //MessageBox.Show("Invalid input " + messageIn);
                return null;
            }
        }

        /// returns the message after convertion
        public Message loadMessage()
        {
            Message msg = convertMessage(this.header + " " + this.body);
            //MessageBox.Show("123 " + msg);
            return msg;
        }

        /// Used to seperate SIR emails and standard emails
        /// works by finding the subject and checking for "SIR"
        /// <param name="messageIn">message string</param>
        private string getEmailType(string messageIn)
        {
            string messageID = messageIn.Substring(0, 10);
            messageIn = messageIn.Substring(11);
            string sender = messageIn.Split(' ')[0];
            string subject = messageIn.Substring(sender.Length + 1, 20);
            int size = subject.Length + sender.Length;
            if (subject.StartsWith("SIR"))
            {
                //messageIn = messageIn.Substring(50);
                //string sortCode = messageIn.Substring(subject.Length + 1, 18);
                //string incident = messageIn.Substring(subject.Length + 10, 22);
                //MessageHolder.SIRcodes.Add(messageID, sortCode);
                //MessageHolder.SIRincidents.Add(messageID, incident);
                return "SIR";
            }
            else
            {
                return "Email";
            }
        }


    }
}
