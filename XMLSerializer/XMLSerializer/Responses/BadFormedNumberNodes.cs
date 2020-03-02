using System.Xml.Serialization;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedNumberNodes
    {
        [XmlElement(ElementName = "test-number-parsing")]
        public TestNumberParsing TestNumberParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-number-parsing")]
    public class TestNumberParsing
    {
        [XmlElement(ElementName = "decimal")]
        public decimal Decimal { get; set; }

        [XmlElement(ElementName = "int")]
        public int Int { get; set; }

        [XmlElement(ElementName = "empty")]
        public int? Empty { get; set; }

        [XmlElement(ElementName = "empty-closed")]
        public int? EmptyClosed { get; set; }
    }
}
