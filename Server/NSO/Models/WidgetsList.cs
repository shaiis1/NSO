using System;
using Newtonsoft.Json;

namespace NSO.Models
{
	public class WidgetsList
	{
        [JsonProperty("widgets")]
        public List<Widget>? Widgets { get; set; }
	}
}

