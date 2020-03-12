using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XNullableInt : IXmlSerializable
    {
        private int? Value { get; set; }

        public bool HasValue
        {
            get
            {
                return Value != null;
            }
        }

        public XNullableInt(int? value)
        {
            Value = value;
        }

        public XNullableInt(string value)
        {
            Value = CustomTypeHelper.DeserializeNullableInt(value);
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
                int? number = CustomTypeHelper.DeserializeNullableInt((string)obj);

                return Value == number;
            }
            else if (obj is double number)
            {
                return Value == number;
            }
            else if (obj is XNullableInt)
            {
                return Value == (XNullableInt)obj;
            }
            else
            {
                return Value == Convert.ToDouble(obj);
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

            Value = CustomTypeHelper.DeserializeNullableInt(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XNullableInt obj1, int? obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XNullableInt obj1, int? obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XNullableInt(string value)
        {
            return new XNullableInt() { Value = CustomTypeHelper.DeserializeNullableInt(value) };
        }

        public static implicit operator XNullableInt(int? value)
        {
            return new XNullableInt() { Value = value };
        }

        public static implicit operator int?(XNullableInt b)
        {
            return b.Value;
        }
    }
}
