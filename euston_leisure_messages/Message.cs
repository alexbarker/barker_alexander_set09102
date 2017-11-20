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
    public abstract class Message
    {
        public string messageID { get; set; }
        public string messageBody { get; set; }
        public string sender { get; set; }

        public abstract MessageReader returnData();


    }
}
