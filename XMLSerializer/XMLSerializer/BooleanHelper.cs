using System;
using System.Linq;

namespace XMLSerializer
{
    public static class BooleanHelper
    {
        private static readonly string[] ValidTrueBool = new string[] { "yes", "1" };
        private static readonly string[] ValidFalseBool = new string[] { "no", "0" };
        private static readonly string[] ValidNullBool = new string[] { "", "null" };

        public static bool DeserializeBoolean(string value)
        {
            value = value.ToLower();

            bool result;
            if (!bool.TryParse(value, out result))
            {
                if (ValidTrueBool.Contains(value))
                {
                    result = true;
                }
                else if (ValidFalseBool.Contains(value))
                {
                    result = false;
                }
                else
                {
                    throw new ArgumentException($"Unknown bool value: {value}");
                }
            }

            return result;
        }

        public static bool? DeserializeNullableBoolean(string value)
        {
            bool? result;

            value = value.ToLower();

            bool parsedResult;
            if (bool.TryParse(value, out parsedResult))
            {
                result = parsedResult;
            }
            else
            {
                if (ValidTrueBool.Contains(value))
                {
                    result = true;
                }
                else if (ValidFalseBool.Contains(value))
                {
                    result = false;
                }
                else if (ValidNullBool.Contains(value))
                {
                    result = null;
                }
                else
                {
                    throw new ArgumentException($"Unknown bool value: {value}");
                }
            }

            return result;
        }
    }
}
