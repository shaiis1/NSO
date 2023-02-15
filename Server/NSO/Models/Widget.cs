using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NSO.Models
{
	public class Widget
	{
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("MagicNumber")]
        public int MagicNumber { get; set; }

        public string? MagicNumberStr { get; set; }

        //public Dictionary<string, string>? Fields {get; set;}
    }
}

