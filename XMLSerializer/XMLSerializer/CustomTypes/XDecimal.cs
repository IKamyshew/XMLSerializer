using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XDecimal : IXmlSerializable
    {
        private decimal Value { get; set; }

        public XDecimal(decimal value)
        {
            Value = value;
        }

        public XDecimal(string value)
        {
            Value = CustomTypeHelper.DeserializeDecimal(value);
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                decimal number = CustomTypeHelper.DeserializeDecimal((string)obj);

                return Value == number;
            }
            else if (obj is decimal number)
            {
                return Value == number;
            }
            else if (obj is XDecimal)
            {
                return Value == (XDecimal)obj;
            }
            else
            {
                return Value == Convert.ToDecimal(obj);
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            string rawValue = reader.ReadElementContentAsString();

            Value = CustomTypeHelper.DeserializeDecimal(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XDecimal obj1, decimal obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XDecimal obj1, decimal obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XDecimal(string value)
        {
            return new XDecimal() { Value = CustomTypeHelper.DeserializeDecimal(value) };
        }

        public static implicit operator XDecimal(decimal value)
        {
            return new XDecimal() { Value = value };
        }

        public static implicit operator decimal(XDecimal b)
        {
            return b.Value;
        }
    }
}
