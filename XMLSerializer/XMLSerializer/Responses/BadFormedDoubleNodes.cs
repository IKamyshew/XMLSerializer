using System.Xml.Serialization;
using XMLSerializer.CustomTypes;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedDoubleNodes
    {
        [XmlElement(ElementName = "test-decimal-parsing")]
        public TestDoubleParsing TestDoubleParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-decimal-parsing")]
    public class TestDoubleParsing
    {
        [XmlElement(ElementName = "decimal")]
        public XDouble Decimal { get; set; }

        [XmlElement(ElementName = "empty")]
        public XNullableDouble Empty { get; set; }
    }
}
