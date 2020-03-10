using System.Xml.Serialization;
using XMLSerializer.CustomTypes;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedBooleanAttributes
    {
        [XmlElement(ElementName = "test-boolean-attribute-parsing")]
        public TestBooleanAttributeParsing TestBooleanParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-boolean-attribute-parsing")]
    public class TestBooleanAttributeParsing
    {
        [XmlAttribute(AttributeName = "yes")]
        public XBool Yes { get; set; }

        [XmlAttribute(AttributeName = "no")]
        public XBool No { get; set; }

        [XmlAttribute(AttributeName = "one")]
        public XBool One { get; set; }

        [XmlAttribute(AttributeName = "zero")]
        public XBool Zero { get; set; }

        [XmlAttribute(AttributeName = "true")]
        public XBool True { get; set; }

        [XmlAttribute(AttributeName = "false")]
        public XBool False { get; set; }

        [XmlAttribute(AttributeName = "empty")]
        public XNullableBool Empty { get; set; }

        [XmlAttribute(AttributeName = "nullable-true")]
        public XNullableBool NullableTrue { get; set; }
    }
}
