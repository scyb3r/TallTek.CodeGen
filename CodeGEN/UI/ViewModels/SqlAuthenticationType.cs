using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CodeGEN.UI.ViewModels
{

    public class SqlAuthenticationType
    {
        public string DisplayName { get; set; }
        public bool RequiresPassword { get; set; }
        public bool IsSelected { get; set; }

        public static ObservableCollection<SqlAuthenticationType> GetStandardAuthenticationTypes()
        {
            ObservableCollection<SqlAuthenticationType> ret = new ObservableCollection<SqlAuthenticationType>();
            ret.Add(new SqlAuthenticationType() { DisplayName = "SQL Authentication", RequiresPassword = true });
            ret.Add( new SqlAuthenticationType() { DisplayName = "Integrated", RequiresPassword = false, IsSelected = true });

            return ret;

        }
    }

}
