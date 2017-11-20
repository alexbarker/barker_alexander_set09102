﻿using System;
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
    /// Version 0.4.4
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// SMS.cs - 
    /// </summary>

    public class SMS : Message
    {
        /// <summary>
        /// validates and adds new SMS messages if possible
        /// </summary>
        /// <param name="messageIn"></param>
        public SMS(String messageIn)
        {
            Regex re = new Regex(@"(S\d{9}) (\+[0-9]+) (.+)");
            Match m = re.Match(messageIn);
            if (m.Success)
            {
                if (m.Groups[2].ToString().Length > 11 && m.Groups[2].ToString().Length < 16 && m.Groups[3].ToString().Length < 140)
                {
                    this.messageID = m.Groups[1].ToString();
                    this.sender = m.Groups[2].ToString();
                    this.messageBody = m.Groups[3].ToString();
                }
                else
                {
                    throw new ArgumentException("Invalid input for SMS");
                }

            }
            else
            {
                throw new ArgumentException("Invalid input for SMS");
            }
        }

        /// <summary>
        /// returns string message format for JSON file writing
        /// </summary>
        /// <returns></returns>
        public override MessageReader returnData()
        {
            return new MessageReader(this.messageID, this.sender + " " + this.messageBody);
        }
    }
}
