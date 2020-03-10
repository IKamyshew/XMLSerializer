using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XBool : IXmlSerializable
    {
        private bool Value { get; set; }

        public XBool(bool value)
        {
           Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is string stringBoolean)
            {
                bool.TryParse(stringBoolean, out bool boolean);
                return Value == boolean;
            }
            else if (obj is bool boolean)
            {
                return Value == boolean;
            }
            else if (obj is XBool serializableBoolean)
            {
                return Value == serializableBoolean.Value;
            }
            else
            {
                return Value == Convert.ToBoolean(obj);
            }
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

            Value = BooleanHelper.ConvertToBoolean(rawValue, falseIfUnknown: false);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XBool obj1, bool obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XBool obj1, bool obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XBool(string value)
        {
            return new XBool() { Value = Convert.ToBoolean(value) };
        }

        public static implicit operator XBool(bool value)
        {
            return new XBool() { Value = value };
        }

        public static implicit operator bool(XBool b)
        {
            return b.Value;
        }
    }
}
