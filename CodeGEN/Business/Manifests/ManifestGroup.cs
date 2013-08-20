using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGEN.Business.Manifests
{
    [Serializable()]
    public class ManifestGroup 
    {
        public string GroupAlias { get; set; }
        public List<ManifestItem> Templates { get; set; }

        public string UIProviderAssemblyName { get; set; }
        public string UIProviderTypeName { get; set; }

        public ManifestGroup()
        {
            this.Templates = new List<ManifestItem>();

        }

    }
}
