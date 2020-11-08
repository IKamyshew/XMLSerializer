using System;
using System.Linq;

namespace XMLSerializer
{
    public static class CustomTypeHelper
    {
        #region bool
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
        #endregion

        #region double
        public static double DeserializeDouble(string value)
        {
            string thousandDelimeter = ",";

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Unable to cast null or empty string to double.");
            }

            value = value.ToLower();

            double result;
            if (!double.TryParse(value, out result))
            {
                value = value.Replace(thousandDelimeter, string.Empty);

                if (!double.TryParse(value, out result))
                {
                    throw new ArgumentException($"Unknown double value: {value}");
                }
            }

            return result;
        }

        public static double? DeserializeNullableDouble(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return DeserializeDouble(value);
        }
        #endregion

        #region decimal
        public static decimal DeserializeDecimal(string value)
        {
            string thousandDelimeter = ",";

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Unable to cast null or empty string to decimal.");
            }

            value = value.ToLower();

            decimal result;
            if (!decimal.TryParse(value, out result))
            {
                value = value.Replace(thousandDelimeter, string.Empty);

                if (!decimal.TryParse(value, out result))
                {
                    throw new ArgumentException($"Unknown decimal value: {value}");
                }
            }

            return result;
        }

        public static decimal? DeserializeNullableDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return DeserializeDecimal(value);
        }
        #endregion

        #region int
        public static int? DeserializeNullableInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return int.Parse(value);
        }
        #endregion
    }
}
