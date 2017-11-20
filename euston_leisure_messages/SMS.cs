using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace euston_leisure_messages
{
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
