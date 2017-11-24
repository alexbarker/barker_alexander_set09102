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
    /// MessageHolder.cs - Stores all aspects of a message in the correct data structures.
    /// </summary>

    public static class MessageHolder
    {
        private static Json js = new Json(); // Json object.
        public static Dictionary<string, Message> messages = new Dictionary<string, Message>(); // Holds all the messages.
        public static int currentEmailID; // Used to sort email messages.
        public static int currentTwitterID; // Used to sort Twitter messages.
        public static int currentSMSID; // Used to dort SMS messages.

        // Hold all the different incident type strings.
        public static Dictionary<string, string> SIRcodes = new Dictionary<string, string>(); // Stores Centre Codes for list output.
        public static Dictionary<string, string> SIRincidents = new Dictionary<string, string>(); // Stores Incident Type for list output.
        public static string[] incidentTypes = { "Theft of Properties", "Staff Attack", "Device Damage", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Sport Injury", "Personal Info Leak" };
        public static MessageRefresher refresher = MessageRefresher.GetInstance(); // Checks for new messages.
        public static Dictionary<string, string> textspeak = new Dictionary<string, string>(); // Textspeak abbrieviations.
        public static Dictionary<string, string> quarantined = new Dictionary<string, string>(); // Quarantined URLs
        public static Dictionary<string, List<string>> mentions = new Dictionary<string, List<String>>(); // Stores all the mentions.
        public static CSVReader r = new CSVReader(); // CSV object.

        //populates the variables in this class when the application starts
        public static void ReadMessages()
        {
            GetTextspeak();
            GetURLs();
            js.fileName = ".\\allMessages.json";
            messages = js.ReadJSON();
            refresher.NumberOfMessages = messages.Count;          
        }

        public static void GetTextspeak()
        {
            string path = ".\\textwords.csv";
            textspeak = r.ReadFile(path);
        }
        
        public static void GetURLs()
        {
            string path = ".\\quarantined.csv";
            quarantined = r.ReadFile(path);
        }

        public static void UpdateURLs()
        {
            string path = ".\\quarantined.csv";
            r.OverwriteFile(path, quarantined);
        }
        
        // Adds new messages to be written to Json file.
        /// <param name="id"> Message ID.</param>
        /// <param name="m"> Message body.</param>
        public static void AddMessage(string id, Message m)
        {
            messages.Add(id, m);
            js.WriteData();
        }  
    }
}
