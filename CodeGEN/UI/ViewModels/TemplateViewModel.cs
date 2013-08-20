using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CodeGEN.UI.ViewModels
{

    public class TemplateViewModel : ViewModelBase
    {

        public TemplateViewModel (FileInfo File)
        {
            this.TemplateName = File.Name;
            this.TemplateAlias = File.Name;
            this.FullFileName = File.FullName;
        }


        private string _TemplateName;
        public string TemplateName
        {
            get { return _TemplateName; }
            set
            {
                _TemplateName = value;
                RaisePropertyChanged("TemplateName");
            }
        }


        //public string TemplateName { get; private set; }
        public string TemplateAlias { get; private set; }
        public string FullFileName { get; private set; }
    }
}
