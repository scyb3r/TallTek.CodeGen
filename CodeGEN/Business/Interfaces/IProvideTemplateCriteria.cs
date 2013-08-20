using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGEN.Business.Interfaces
{
    public interface IProvideTemplateCriteria
    {
        List<KeyValuePair<string, string>> GetTemplateCriteria();
    }
}
