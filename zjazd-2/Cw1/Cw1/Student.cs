using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Cw1
{
    [XmlRoot(ElementName = "student")]

    public class Student
    {
        
        [XmlAttribute(AttributeName = "indexNumber")]
        [JsonProperty("indexNumber")]

        public string Index { get; set; }

        [XmlElement(ElementName = "fname")]
        public string Fname { get; set; }

        [XmlElement(ElementName = "lname")]
        public string Lname { get; set; }

        [XmlElement(ElementName = "birthdate")]
        public string Birthdate { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "mothersName")]
        public string MothersName { get; set; }

        [XmlElement(ElementName = "fathersName")]
        public string FathersName { get; set; }

        [XmlElement(ElementName = "studies")]
        public Studies Studies { get; set; }

    }
}
