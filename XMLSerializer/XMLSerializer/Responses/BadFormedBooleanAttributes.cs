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
        public XBoolean Yes { get; set; }

        [XmlAttribute(AttributeName = "no")]
        public XBoolean No { get; set; }

        [XmlAttribute(AttributeName = "one")]
        public XBoolean One { get; set; }

        [XmlAttribute(AttributeName = "zero")]
        public XBoolean Zero { get; set; }

        [XmlAttribute(AttributeName = "true")]
        public XBoolean True { get; set; }

        [XmlAttribute(AttributeName = "false")]
        public XBoolean False { get; set; }

        [XmlAttribute(AttributeName = "empty")]
        public XNullableBoolean Empty { get; set; }

        [XmlAttribute(AttributeName = "null")]
        public XNullableBoolean Null { get; set; }

        [XmlAttribute(AttributeName = "nullable-true")]
        public XNullableBoolean NullableTrue { get; set; }
    }
}
