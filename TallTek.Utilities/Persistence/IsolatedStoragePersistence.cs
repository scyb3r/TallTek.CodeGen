using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallTek.Utilities.Persistence
{
    [Serializable]
    public class IsolatedStoragePersistence<T>
    // where T : IPersistProperties
    {
        public IsolatedStoragePersistence()
        {

        }

        private static string IsoFileName
        {
            get { return string.Format("{0}.{1}.dat", typeof(T).DeclaringType, typeof(T).Name); }
        }

        public void SaveSettings()
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (System.IO.IsolatedStorage.IsolatedStorageFileStream isfs = new System.IO.IsolatedStorage.IsolatedStorageFileStream(IsoFileName, System.IO.FileMode.Truncate))
            {
                xs.Serialize(isfs, this);
            }
        }

        public static T LoadProperties()
        {
            //IPersistProperties retVal = null; // = new IPersistProperties();
            object retVal = null;

            try
            {
                using (System.IO.IsolatedStorage.IsolatedStorageFileStream isfs = new System.IO.IsolatedStorage.IsolatedStorageFileStream(IsoFileName, System.IO.FileMode.OpenOrCreate))
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
            }
            catch (Exception ex)
            {
                //TODO: dont catch

            }

            return (T)retVal;
        }
    }
}
