using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace QueueApp.Common
{
    public class XmlProvider
    {
        #region XML operations
        public String Serialize(Packet packet)
        {
            if(packet == null)
            {
                throw new ArgumentNullException("Paket ne moze biti null.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Packet));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, packet);
                    return sww.ToString();
                }
            }
        }
        public Packet Deserialize(String text)
        {
            if(text == null)
            {
                throw new ArgumentNullException("poruka ne moze biti null.");
            }

            if(text == "")
            {
                throw new ArgumentException("Poruka ne moze biti prazna.");
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(Packet));

            using (var sww = new StringReader(text))
            {
                using (var writer = XmlReader.Create(sww))
                {
                    return (Packet)deserializer.Deserialize(writer);
                }
            }
        }
        #endregion
    }
}
