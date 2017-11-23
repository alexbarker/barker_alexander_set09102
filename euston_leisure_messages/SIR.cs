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
    /// Version 0.5.0
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 22th November 2017
    /// </summary>
    /// <summary>
    /// SIR.cs - This class is used to process, store and validate SIR Email messages.
    /// </summary>

    public class SIR : Email
    {
        public string natureOfIncident { get; set; }
        public string centCode { get; set; }
        public string dateReported { get; set; }     

        /// <summary>
        /// Validates Centre Code and Incident Type.
        /// </summary>
        /// <param name="messageIn"> Message body.</param>
        public SIR(string messageIn) : base(messageIn)
        {
            Regex regex = new Regex(@"Cent Code: ([0-9-]+)"); // Searches message body for centre code.
            Match match = regex.Match(messageIn);
            if (match.Success)
            {
                this.centCode = match.Groups[1].Value;
                MessageHolder.SIRcodes.Add(messageID, centCode); // Adds the code to a dictionary for SIR list display.
            }
            else
            {
                //throw new ArgumentException("Centre Code not found!");
                MessageBox.Show("Invalid Centre Code!");
                return;
            }

            regex = new Regex(@"Nature of Incident: (.+)"); // Searches message body for incident type.
            match = regex.Match(messageIn);
            if (match.Success)
            {
                string nature = match.Groups[1].ToString();
                foreach (string n in MessageHolder.incidentTypes) // Confirms incident type exists.
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
                throw new ArgumentException("Incident Type not found!");
            }

            regex = new Regex(@"SIR (.+)"); // Searches message body for report date.
            match = regex.Match(messageIn);
                if (match.Success)
                {
                dateReported = match.Groups[1].Value;
                }
                else
                {
                    throw new ArgumentException("Date not found!");
                }
        }
    }
}
