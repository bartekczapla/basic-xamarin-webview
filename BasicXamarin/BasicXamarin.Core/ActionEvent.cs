using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicXamarin.Core
{
    [JsonObject]
    public class ActionEvent
    {

        [JsonProperty("action", Required = Required.Always)]
        public string Action { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
