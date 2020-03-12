using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer.CustomTypes
{
    [DebuggerDisplay("{Value}")]
    public struct XNullableBool : IXmlSerializable
    {
        private bool? Value { get; set; }

        public bool HasValue {
            get
            {
                return Value != null;
            }
        }

        public XNullableBool(bool? value)
        {
            Value = value;
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

            if (obj is string stringBoolean)
            {
                bool.TryParse(stringBoolean, out bool boolean);
                return Value.Value == boolean;
            }
            else if (obj is bool boolean)
            {
                return Value.Value == boolean;
            }
            else if (obj is XNullableBool serializableBoolean)
            {
                return Value.Value == serializableBoolean.Value;
            }
            else
            {
                return Value.Value == Convert.ToBoolean(obj);
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

            Value = CustomTypeHelper.DeserializeNullableBoolean(rawValue);
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(XNullableBool obj1, bool obj2)
        {
            return obj1.Value.Equals(obj2);
        }

        public static bool operator !=(XNullableBool obj1, bool obj2)
        {
            return !obj1.Value.Equals(obj2);
        }

        public static implicit operator XNullableBool(string value)
        {
            return new XNullableBool() { Value = Convert.ToBoolean(value) };
        }

        public static implicit operator XNullableBool(bool value)
        {
            return new XNullableBool() { Value = value };
        }

        public static implicit operator bool?(XNullableBool b)
        {
            return b.Value;
        }
    }
}
