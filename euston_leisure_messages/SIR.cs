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
    /// SIR.cs - 
    /// </summary>

    public class SIR : Email
    {
        public DateTime dateReported { get; set; }
        public string natureOfIncident { get; set; }
        public string centCode { get; set; }       

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
            Regex regex = new Regex(@"Cent Code: ([0-9-]+)"); //finds the sort code in the message body
            Match match = regex.Match(messageIn);
            if (match.Success)
            {
                this.centCode = match.Groups[1].Value;
                MessageHolder.SIRcodes.Add(messageID, centCode);
                //MessageHolder.SIRcodes.Add(messageID, sortCode);
                //MessageHolder.SIRincidents.Add(messageID, incident);
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
                        MessageHolder.SIRincidents.Add(messageID, natureOfIncident);
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
