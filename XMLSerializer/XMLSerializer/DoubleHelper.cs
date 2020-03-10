using System;

namespace XMLSerializer
{
    public static class DoubleHelper
    {
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
    }
}
