using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XNullableDouble : IXmlSerializable
    {
        private double? Value { get; set; }

        public bool HasValue
        {
            get
            {
                return Value != null;
            }
        }

        public XNullableDouble(double value)
        {
            Value = value;
        }

        public XNullableDouble(string value)
        {
            Value = CustomTypeHelper.DeserializeNullableDouble(value);
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
                double number = CustomTypeHelper.DeserializeDouble((string)obj);

                return Value == number;
            }
            else if (obj is double number)
            {
                return Value == number;
            }
            else if (obj is XNullableDouble)
            {
                return Value == (XNullableDouble)obj;
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

            Value = CustomTypeHelper.DeserializeNullableDouble(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XNullableDouble obj1, double? obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XNullableDouble obj1, double? obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XNullableDouble(string value)
        {
            return new XNullableDouble() { Value = CustomTypeHelper.DeserializeNullableDouble(value) };
        }

        public static implicit operator XNullableDouble(double? value)
        {
            return new XNullableDouble() { Value = value };
        }

        public static implicit operator double?(XNullableDouble b)
        {
            return b.Value;
        }
    }
}