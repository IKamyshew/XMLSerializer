using System.Xml.Serialization;
using XMLSerializer.CustomTypes;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedDecimalNodes
    {
        [XmlElement(ElementName = "test-decimal-parsing")]
        public TestDecimalParsing TestDecimalParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-decimal-parsing")]
    public class TestDecimalParsing
    {
        [XmlElement(ElementName = "decimal")]
        public XDecimal Decimal { get; set; }

        [XmlElement(ElementName = "decimal-thousand")]
        public XDecimal DecimalThousand { get; set; }

        [XmlElement(ElementName = "real-decimal")]
        public XNullableDecimal RealDecimal { get; set; }

        [XmlElement(ElementName = "empty")]
        public XNullableDecimal Empty { get; set; }
    }
}
