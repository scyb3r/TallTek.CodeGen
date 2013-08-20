using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGEN.Business.Manifests
{
    [Serializable()]
    public class ManifestItem
    {
        public string TemplateFileName { get; set; }
        public string TemplateAlias { get; set; }

    }
}
