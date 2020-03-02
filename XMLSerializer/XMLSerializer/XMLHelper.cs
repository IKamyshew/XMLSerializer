using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLSerializer
{
    public static class XmlHelper
    {
        /// <summary>
        /// Deserializes XmlDocument object to Serializable object of type T.
        /// </summary>
        /// <typeparam name="T">Serializable object type as output type.</typeparam>
        /// <param name="xml">XmlDocument object to be deserialized.</param>
        /// <returns>Deserialized serializable object of type T.</returns>
        public static T Deserialize<T>(this XmlDocument xml) where T : class
        {
            T response = null;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (XmlReader reader = new XmlNodeReader(xml))
            {
                response = (T)serializer.Deserialize(reader);
            }

            return response;
        }

        /// <summary>
        /// Deserializes XDocument object to Serializable object of type T.
        /// </summary>
        /// <typeparam name="T">Serializable object type as output type.</typeparam>
        /// <param name="xml">XDocument object to be deserialized.</param>
        /// <returns>Deserialized serializable object of type T.</returns>
        public static T Deserialize<T>(this XDocument doc) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = doc.Root.CreateReader())
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }
    }
}
