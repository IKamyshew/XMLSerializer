using System.Xml.Serialization;
using XMLSerializer.CustomTypes;

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
        [XmlElement(ElementName = "empty-closed")]
        public XNullableInt EmptyClosed { get; set; }

        [XmlElement(ElementName = "empty")]
        public XNullableInt Empty { get; set; }

        [XmlElement(ElementName = "valid")]
        public XNullableInt Valid { get; set; }
    }
}
