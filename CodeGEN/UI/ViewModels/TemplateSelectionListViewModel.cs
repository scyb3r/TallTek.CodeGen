using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CodeGEN.UI.ViewModels
{
    public class TemplateSelectionListViewModel : ViewModelBase
    {
        public TemplateSelectionListViewModel()
        {
            this.AvailableTemplates = new List<SelectableObject<TemplateViewModel>>();
        }


        private List<SelectableObject<TemplateViewModel>> _AvailableTemplates;
        public List<SelectableObject<TemplateViewModel>> AvailableTemplates
        {
            get { return _AvailableTemplates; }
            set
            {
                _AvailableTemplates = value;
                RaisePropertyChanged("AvailableTemplates");
            }
        }



        public void LoadTemplatesFromFolder(string FolderName)
        {
            List<SelectableObject<TemplateViewModel>> tmpList = new List<SelectableObject<TemplateViewModel>>();
            DirectoryInfo di = new DirectoryInfo(FolderName);
            di.GetFiles().ToList().ForEach(o=> 
                {
                    var tmp = new TemplateViewModel(o);
                    tmpList.Add(new SelectableObject<TemplateViewModel>(tmp));
                }
                
                );

            this.AvailableTemplates = tmpList;

        }

        public List<TemplateViewModel> GetSelectedTemplates()
        {

            var templates = this.AvailableTemplates.Where(o => o.IsSelected == true).Select(o=> o.SelectedObject).ToList();
            

            return templates;

        }


    }



}
