using System.Xml.Serialization;

namespace Cw1
{
    public class Studies
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "mode")]
        public Mode Mode { get; set; }
    }
}