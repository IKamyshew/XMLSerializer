using System.Xml.Serialization;
using XMLSerializer.CustomTypes;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedBooleanNodes
    {
        [XmlElement(ElementName = "test-boolean-parsing")]
        public TestBooleanParsing TestBooleanParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-boolean-parsing")]
    public class TestBooleanParsing
    {
        [XmlElement(ElementName = "yes")]
        public XBoolean Yes { get; set; }

        [XmlElement(ElementName = "no")]
        public XBoolean No { get; set; }

        [XmlElement(ElementName = "one")]
        public XBoolean One { get; set; }

        [XmlElement(ElementName = "zero")]
        public XBoolean Zero { get; set; }

        [XmlElement(ElementName = "true")]
        public XBoolean True { get; set; }

        [XmlElement(ElementName = "false")]
        public XBoolean False { get; set; }

        [XmlElement(ElementName = "empty")]
        public XNullableBoolean Empty { get; set; }

        [XmlElement(ElementName = "empty-closed")]
        public XNullableBoolean EmptyClosed { get; set; }

        [XmlElement(ElementName = "null")]
        public XNullableBoolean Null { get; set; }

        [XmlElement(ElementName = "nullable-true")]
        public XNullableBoolean NullableTrue { get; set; }
    }
}
