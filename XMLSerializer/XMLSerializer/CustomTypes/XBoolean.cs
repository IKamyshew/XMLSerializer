using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XBoolean : IXmlSerializable
    {
        private bool Value { get; set; }

        public XBoolean(bool value)
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
            else if (obj is XBoolean serializableBoolean)
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

            Value = BooleanHelper.DeserializeBoolean(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XBoolean obj1, bool obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XBoolean obj1, bool obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XBoolean(string value)
        {
            return new XBoolean() { Value = Convert.ToBoolean(value) };
        }

        public static implicit operator XBoolean(bool value)
        {
            return new XBoolean() { Value = value };
        }

        public static implicit operator bool(XBoolean b)
        {
            return b.Value;
        }
    }
}
