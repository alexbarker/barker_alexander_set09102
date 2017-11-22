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
    /// Version 0.4.5
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// Json.cs - 
    /// </summary>

    class Json
    {
        //sets the file to the filename specified inside the application properties
        public string fileName { get; set; } = ".\\allMessages.json";



        /// used to read in the JSON file and deserialize the objects found inside it     
        public Dictionary<string, Message> readJSON()
        {
            Dictionary<string, Message> messages = new Dictionary<string, Message>();
            // try
            // {

            MessageReader convert = new MessageReader();
               // if (this.fileName != null)
               // {
                    string jsonText = System.IO.File.ReadAllText(this.fileName);
                    JObject jsonData = JObject.Parse(jsonText);
                    IList<JToken> results = jsonData["allMessages"].Children().ToList();
                    IList<MessageReader> output = new List<MessageReader>();
                    foreach (JToken result in results)
                    {
                        Message msg = JsonConvert.DeserializeObject<MessageReader>(result.ToString()).loadMessage();
                        //msg.seen = true;
                        messages.Add(msg.messageID, msg);
                    }
               // }
           // }
           // catch (Exception ex)
           // {
            //    MessageBox.Show(ex.ToString());

                //prompt the user to change the file path as the one being used is invalid
            //    if (!Settings.isOpen)
            //    {
             //       Window settings = new Settings();
             //       settings.Show();
             //       Settings.isOpen = true;
              //  }

            //}
            return messages;
        }

        /// users to overwrite the file if new messages are added, data source is the MessageHolder messages 
        public void writeData()
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
