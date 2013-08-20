using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CodeGEN.Business.TemplateGeneration
{
    public class TemplateGenerationEngine
    {

        public static string ProcessTemplate(string templateFile, List<KeyValuePair<string,string>> templateProperties)
        {

            string fileOutput = null;
            Microsoft.VisualStudio.TextTemplating.Engine e = new Microsoft.VisualStudio.TextTemplating.Engine();
            using (TemplateGenerationHost host = new TemplateGenerationHost())
            {
                foreach (var tp in templateProperties)
                {
                    if (tp.Value != null) CallContext.LogicalSetData(tp.Key.ToString(), tp.Value.ToString());
                }
            
                host.TemplateFileValue = templateFile;

                string fileContents = File.ReadAllText(templateFile);
                fileOutput = e.ProcessTemplate(fileContents, host);

                if (host.Errors.HasErrors)
                {
                    fileOutput = string.Empty;

                    foreach (var err in host.Errors)
                    {
                        fileOutput += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
                        fileOutput += err;
                    }
                }

                //clear out the context data we used in the template
                //SetCallContextData(objProperties, true);
            }
            return fileOutput;

        }

        public static string ProcessTemplate(string templateFile, IMarshalProperties objProperties)
        {
            string fileOutput = null;
            Microsoft.VisualStudio.TextTemplating.Engine e = new Microsoft.VisualStudio.TextTemplating.Engine();
            using (TemplateGenerationHost host = new TemplateGenerationHost())
            {
                SetCallContextData(objProperties);
                host.TemplateFileValue = templateFile;

                string fileContents = File.ReadAllText(templateFile);
                fileOutput = e.ProcessTemplate(fileContents, host);

                if (host.Errors.HasErrors)
                {
                    fileOutput = string.Empty;

                    foreach (var err in host.Errors)
                    {
                        fileOutput += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
                        fileOutput += err;
                    }
                }

                //clear out the context data we used in the template
                SetCallContextData(objProperties, true);
            }
            return fileOutput;
        }

        private static void SetCallContextData(IMarshalProperties objProperties, bool clearContextData = false)
        {
            foreach (PropertyInfo pi in objProperties.GetType().GetProperties())
            {
                object val = pi.GetValue(objProperties, null);

                if (val != null)
                {
                    if (val is System.Collections.ICollection)
                    {
                        foreach (var innerObj in (ICollection)val)
                        {
                            if (innerObj is DictionaryEntry)
                            {
                                DictionaryEntry de = (DictionaryEntry)innerObj;
                                CallContext.LogicalSetData(de.Key.ToString(), de.Value.ToString());
                            }
                            else if (val is List<string>)
                            {
                                string serializedValue = null;
                                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    xs.Serialize(ms, val);
                                    serializedValue = ms.ToString();

                                    ms.Position = 0;
                                    StreamReader sr = new StreamReader(ms);
                                    serializedValue = sr.ReadToEnd();
                                    CallContext.LogicalSetData(pi.Name, serializedValue);
                                }
                            }
                        }
                    }

                    else
                    {
                        System.Runtime.Remoting.Messaging.CallContext.LogicalSetData(pi.Name, (clearContextData) ? null : val.ToString());
                    }
                }
            }
        }
    }
}
