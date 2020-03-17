using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Cw1
{
    [JsonObjectAttribute(Title = "Uczelnia")]
    public class Uczelnia
    {
        [XmlAttribute(AttributeName = "createdAt")]
        public string CreatedAt { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        [XmlArray("studenci")]
        [XmlArrayItem("student")]
        [JsonProperty("studenci")]

        public List<Student> Students { get; set; }


        [XmlArray("activeStudies")]
        [XmlArrayItem("studies")]
        public List<ActiveStudieses> ActiveStudieses { get; set; }

    }
}
