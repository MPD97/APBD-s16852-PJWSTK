using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cw1
{
    public class Studies
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Mode Mode { get; set; }
    }
}