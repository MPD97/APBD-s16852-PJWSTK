using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace Cw1
{
    public class Uczelnia
    {
        [XmlAttribute(AttributeName = "createdAt")]
        public string CreatedAt { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "studenci")]
        //[XmlArray("TeamMembers")]

        public List<Student> Students { get; set; }
    }
}
