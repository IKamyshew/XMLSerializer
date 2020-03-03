using System.Xml.Serialization;

namespace XMLSerializer.Responses
{
    [XmlRoot(ElementName = "interface-response")]
    public class BadFormedBooleanAttributesIgnoreProperties
    {
        [XmlElement(ElementName = "test-boolean-attribute-parsing")]
        public TestBooleanAttributeParsingIgnoreProperties TestBooleanParsing { get; set; }
    }

    [XmlRoot(ElementName = "test-boolean-attribute-parsing")]
    public class TestBooleanAttributeParsingIgnoreProperties
    {
        [XmlAttribute(AttributeName = "yes")]
        public string YesAsString {
            get
            {
                return this.Yes.ToString().ToLower();
            }
            set
            {
                this.Yes = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "no")]
        public string NoAsString
        {
            get
            {
                return this.No.ToString().ToLower();
            }
            set
            {
                this.No = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "one")]
        public string OneAsString
        {
            get
            {
                return this.One.ToString().ToLower();
            }
            set
            {
                this.One = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "zero")]
        public string ZeroAsString
        {
            get
            {
                return this.Zero.ToString().ToLower();
            }
            set
            {
                this.Zero = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "true")]
        public string TrueAsString
        {
            get
            {
                return this.True.ToString().ToLower();
            }
            set
            {
                this.True = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "false")]
        public string FalseAsString
        {
            get
            {
                return this.False.ToString().ToLower();
            }
            set
            {
                this.False = BooleanHelper.ConvertToBoolean(value, falseIfUnknown: false);
            }
        }

        [XmlAttribute(AttributeName = "empty")]
        public string EmptyAsString
        {
            get
            {
                return this.Empty.ToString().ToLower();
            }
            set
            {
                this.Empty = BooleanHelper.DeserializeNullableBoolean(value);
            }
        }

        [XmlAttribute(AttributeName = "nullable-true")]
        public string NullableTrueAsString
        {
            get
            {
                return this.NullableTrue.ToString().ToLower();
            }
            set
            {
                this.NullableTrue = BooleanHelper.DeserializeNullableBoolean(value);
            }
        }

        public bool Yes { get; set; }
        public bool No { get; set; }
        public bool One { get; set; }
        public bool Zero { get; set; }
        public bool True { get; set; }
        public bool False { get; set; }
        public bool? Empty { get; set; }
        public bool? NullableTrue { get; set; }
    }
}
