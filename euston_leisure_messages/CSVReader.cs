using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    /// CSVReader.cs - This class adds the ability to read and write to CSV files.
    /// </summary>

    public class CSVReader
    {
        // Populates the CSV reader dictionary.
        /// <param name="fileName">Gets the path name.</param>
        public Dictionary<string, string> ReadFile(string fileName)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

                string csvText = System.IO.File.ReadAllText(fileName);

                using (System.IO.StringReader reader = new System.IO.StringReader(csvText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] vals = line.Split(',');
                        output.Add(vals[0], line.Substring(vals[0].Length + 1));
                    }
                }
            return output;
        }

        // Used to write the whole dictionary to file.
        /// <param name="fileName">Gets the path name.</param>
        /// <param name="data">Collects dictionary item to write to file.</param>
        public void OverwriteFile(string fileName, Dictionary<string, string> data)
        {
            string output = "";
            foreach (string key in data.Keys)
            {
                output += key + "," + data[key] + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.Write(output);
            }
        }
    }
}
