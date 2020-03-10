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
        public XBool Yes { get; set; }

        [XmlElement(ElementName = "no")]
        public XBool No { get; set; }

        [XmlElement(ElementName = "on")]
        public XBool On { get; set; }

        [XmlElement(ElementName = "off")]
        public XBool Off { get; set; }

        [XmlElement(ElementName = "enabled")]
        public XBool Enabled { get; set; }

        [XmlElement(ElementName = "disabled")]
        public XBool Disabled { get; set; }

        [XmlElement(ElementName = "one")]
        public XBool One { get; set; }

        [XmlElement(ElementName = "zero")]
        public XBool Zero { get; set; }

        [XmlElement(ElementName = "true")]
        public XBool True { get; set; }

        [XmlElement(ElementName = "false")]
        public XBool False { get; set; }

        [XmlElement(ElementName = "empty")]
        public XNullableBool Empty { get; set; }

        [XmlElement(ElementName = "empty-closed")]
        public XNullableBool EmptyClosed { get; set; }

        [XmlElement(ElementName = "nullable-true")]
        public XNullableBool NullableTrue { get; set; }
    }
}
