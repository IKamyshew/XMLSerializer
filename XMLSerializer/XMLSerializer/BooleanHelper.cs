using System;
using System.Linq;

namespace XMLSerializer
{
    public static class BooleanHelper
    {
        private static readonly string[] ValidTrueBool = new string[] { "yes", "1" };
        private static readonly string[] ValidFalseBool = new string[] { "no", "0" };

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

        public static Boolean ConvertToBoolean(String val, bool falseIfUnknown = true)
        {
            try
            {
                if (val == "1" ||
                    val.Equals("on", StringComparison.CurrentCultureIgnoreCase) ||
                    val.Equals("True", StringComparison.CurrentCultureIgnoreCase) ||
                    val.Equals("Enabled", StringComparison.CurrentCultureIgnoreCase) ||
                    val.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
                else if (val == "0" ||
                         val.Equals("off", StringComparison.CurrentCultureIgnoreCase) ||
                         val.Equals("False", StringComparison.CurrentCultureIgnoreCase) ||
                         val.Equals("Disabled", StringComparison.CurrentCultureIgnoreCase) ||
                         val.Equals("No", StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }

                if (falseIfUnknown)
                {
                    return false;
                }
                else
                {
                    throw new ArgumentException($"Unknown bool value: {val}");
                }
            }
            catch
            {
                if (falseIfUnknown)
                {
                    return false;
                }
                else
                {
                    throw new ArgumentException($"Unknown bool value: {val}");
                }
            }
        }

        public static bool? DeserializeNullableBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return (bool?) ConvertToBoolean(val: value, falseIfUnknown: false);
        }
    }
}
