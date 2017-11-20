using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace euston_leisure_messages
{
    public class SIR : Email
    {
        public DateTime dateReported { get; set; }
        public string natureOfIncident { get; set; }
        public string sortCode { get; set; }

        /// <summary>
        /// uses the email class to perform normal translation to email then validates sort code, incident type
        /// date reported etc
        /// </summary>
        /// <param name="messageIn">raw message string</param>
        public SIR(string messageIn) : base(messageIn)
        {
            DateTime reportDate;
            DateTime.TryParse(this.subject.Substring(4, 8), out reportDate);
            this.dateReported = reportDate;
            Regex regex = new Regex(@"Sort Code: ([0-9-]+)"); //finds the sort code in the message body
            Match match = regex.Match(messageIn);
            if (match.Success)
            {
                this.sortCode = match.Groups[1].Value;
            }
            else
            {
                throw new ArgumentException("no centre code found");
            }


            regex = new Regex(@"Nature of Incident: (.+)"); //finds the incident type in the message body
            match = regex.Match(messageIn);
            if (match.Success)
            {
                string nature = match.Groups[1].ToString();
                foreach (string n in MessageHolder.incidentTypes) //checks if incident type exists in the current array of types
                {
                    if (nature.StartsWith(n))
                    {
                        this.natureOfIncident = n;
                        break;
                    }
                }
            }
            else
            {
                throw new ArgumentException("no matching incident found");
            }


        }
    }
}
