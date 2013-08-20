using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGEN.Business.Manifests
{
    [Serializable]
    public class ProjectManifest : TallTek.Utilities.Persistence.FileStoragePersistence<ProjectManifest>
    {

        public string ProjectManifestFileName { get; set; }
        public string ProjectManifestAlias { get; set; }
        public List<ManifestGroup> ProjectTemplateGroups { get; set; }

        public ProjectManifest()
        {
            this.ProjectTemplateGroups = new List<ManifestGroup>();

        }

    }
}
