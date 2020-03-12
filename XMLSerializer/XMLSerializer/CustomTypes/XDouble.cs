using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XDouble : IXmlSerializable
    {
        private double Value { get; set; }

        public XDouble(double value)
        {
            Value = value;
        }

        public XDouble(string value)
        {
            Value = CustomTypeHelper.DeserializeDouble(value);
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                double number = CustomTypeHelper.DeserializeDouble((string)obj);

                return Value == number;
            }
            else if (obj is double number)
            {
                return Value == number;
            }
            else if (obj is XDouble)
            {
                return Value == (XDouble) obj;
            }
            else
            {
                return Value == Convert.ToDouble(obj);
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

            Value = CustomTypeHelper.DeserializeDouble(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XDouble obj1, double obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XDouble obj1, double obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XDouble(string value)
        {
            return new XDouble() { Value = CustomTypeHelper.DeserializeDouble(value) };
        }

        public static implicit operator XDouble(double value)
        {
            return new XDouble() { Value = value };
        }

        public static implicit operator double(XDouble b)
        {
            return b.Value;
        }
    }
}
