using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedArrayNodes
    {
        [XmlElement(ElementName = "errors")]
        public ErrorsArray Errors { get; set; }

        [XmlRoot(ElementName = "errors")]
        public class ErrorsArray
        {
            [XmlElement(ElementName = "error")]
            [XmlChoiceIdentifier("EnumErrors")]
            public List<string> Error { get; set; }


            // Don't serialize this field. The EnumType field
            // contains the enumeration value that corresponds
            // to the MyChoice field value.
            [XmlIgnore]
            public ErrorsEnum EnumErrors;
        }
    }

    public enum ErrorsEnum
    {
        Error1,
        Error2,
        Error3,
        Error4,
        Error5,
        Error6,
        Error7,
        Error8,
        Error9,
        Error10
    }
}
