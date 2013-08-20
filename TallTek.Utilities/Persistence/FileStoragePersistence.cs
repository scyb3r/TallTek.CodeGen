using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallTek.Utilities.Persistence
{
    public class FileStoragePersistence<T>
    {
        public FileStoragePersistence()
        {

        }

        public void SaveSettings(string fileName)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            if (!File.Exists(fileName)) { using (File.Create(fileName)) { } }

            using (FileStream isfs = new FileStream(fileName, System.IO.FileMode.Truncate))
            {
                xs.Serialize(isfs, this);
            }
        }

        public static T LoadProperties(string fileName)
        {
            object retVal = null;

            using (FileStream isfs = new FileStream(fileName, System.IO.FileMode.OpenOrCreate))
            {
                if (isfs.Length > 0)
                {
                    //if you get deserialization / xml errors - the following can be helpful
                    //StreamReader sr = new StreamReader(isfs);
                    //string output = sr.ReadToEnd();

                    isfs.Position = 0;
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    retVal = (T)xs.Deserialize(isfs);
                }
            }

            return (T)retVal;
        }
    }
}
