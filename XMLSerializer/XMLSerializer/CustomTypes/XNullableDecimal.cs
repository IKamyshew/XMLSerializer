using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XNullableDecimal : IXmlSerializable
    {
        private decimal? Value { get; set; }

        public bool HasValue
        {
            get
            {
                return Value != null;
            }
        }

        public XNullableDecimal(decimal value)
        {
            Value = value;
        }

        public XNullableDecimal(string value)
        {
            Value = CustomTypeHelper.DeserializeNullableDecimal(value);
        }

        public override bool Equals(object obj)
        {
            if (!Value.HasValue)
            {
                return obj == null;
            }

            if (obj == null)
            {
                return !Value.HasValue;
            }

            if (obj is string)
            {
                decimal number = CustomTypeHelper.DeserializeDecimal((string)obj);

                return Value == number;
            }
            else if (obj is decimal number)
            {
                return Value == number;
            }
            else if (obj is XNullableDecimal)
            {
                return Value == (XNullableDecimal)obj;
            }
            else
            {
                return Value == Convert.ToDecimal(obj);
            }
        }

        public override string ToString()
        {
            return Value.HasValue ? Value.ToString() : string.Empty;
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

            Value = CustomTypeHelper.DeserializeNullableDecimal(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XNullableDecimal obj1, decimal? obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XNullableDecimal obj1, decimal? obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XNullableDecimal(string value)
        {
            return new XNullableDecimal() { Value = CustomTypeHelper.DeserializeNullableDecimal(value) };
        }

        public static implicit operator XNullableDecimal(decimal? value)
        {
            return new XNullableDecimal() { Value = value };
        }

        public static implicit operator decimal?(XNullableDecimal b)
        {
            return b.Value;
        }
    }
}
