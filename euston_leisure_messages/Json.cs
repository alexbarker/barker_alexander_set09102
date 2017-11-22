using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

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
    /// Json.cs - This class is responsible for reading and writing to Json file. 
    /// </summary>

    class Json
    {
        /// Adds the Json filename to a variable.
        public string fileName { get; set; } = ".\\allMessages.json";

        /// Reads/Converts the Json file and adds to dictionaries.    
        public Dictionary<string, Message> ReadJSON()
        {
            Dictionary<string, Message> messages = new Dictionary<string, Message>();

            MessageReader convert = new MessageReader();
                    string jsonText = System.IO.File.ReadAllText(this.fileName);
                    JObject jsonData = JObject.Parse(jsonText);
                    IList<JToken> results = jsonData["allMessages"].Children().ToList();
                    IList<MessageReader> output = new List<MessageReader>();
                    foreach (JToken result in results)
                    {
                        Message msg = JsonConvert.DeserializeObject<MessageReader>(result.ToString()).LoadMessage();
                        messages.Add(msg.messageID, msg);
                    }
            return messages;
        }

        /// Adds new message to the Json file.
        public void WriteData()
        {
            List<MessageReader> output = new List<MessageReader>();
            foreach (Message m in MessageHolder.messages.Values)
            {
                output.Add(m.returnData());
            }
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(this.fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                sw.WriteLine("{\"allMessages\": ");
                sw.WriteLine(JsonConvert.SerializeObject(output, Formatting.Indented));
                sw.WriteLine("}");
            }
        }
    }
}
