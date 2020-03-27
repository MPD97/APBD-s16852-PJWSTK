using System.Xml.Serialization;

namespace Cw1
{
    public class ActiveStudieses
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "numberOfStudents")]
        public int NumberOfStudents { get; set; } = 1;
    }
}