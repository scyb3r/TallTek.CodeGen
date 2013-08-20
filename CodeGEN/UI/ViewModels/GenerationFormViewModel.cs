using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeGEN.Business.Interfaces;
using CodeGEN.Business.Manifests;
using CodeGEN.Business.TemplateGeneration;
using CodeGEN.UI.UserControls;
using CodeGEN.UI.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CodeGEN.UI.ViewModels
{
    public class GenerationFormViewModel : ViewModelBase
    {
        public GenerationFormViewModel()
        {

            this.TemplateList = new TemplateSelectionListViewModel();
            TemplateList.LoadTemplatesFromFolder(@"e:\SkyDrive\Code\Projects\TalTek\CodeGEN\Templates\");

            //  this.SolutionManifest = ProjectManifest.LoadProperties("test.cgsav");

            this.SolutionManifest = new ProjectManifest();
            this.SolutionManifest.ProjectManifestAlias = "Test manifest";
            this.SolutionManifest.ProjectManifestFileName = "_testManifes.cg";


            ManifestGroup grp = new ManifestGroup();
            grp.GroupAlias = "Test 1234";
            grp.UIProviderAssemblyName = "";
            grp.UIProviderTypeName = "?";

            ManifestItem mi1 = new ManifestItem();
            mi1.TemplateFileName = "c:\template1";
            mi1.TemplateAlias = "Template 1 Alias";
            grp.Templates.Add(mi1);

            this.SolutionManifest.ProjectTemplateGroups.Add(grp);


            ManifestGroup grp2 = new ManifestGroup();
            grp2.GroupAlias = "Test 32";
            grp2.UIProviderAssemblyName = "";
            grp2.UIProviderTypeName = "?";

            ManifestItem mi2 = new ManifestItem();
            mi2.TemplateFileName = "c:\template1";
            mi2.TemplateAlias = "Template 1 Alias";
            grp2.Templates.Add(mi2);
            this.SolutionManifest.ProjectTemplateGroups.Add(grp2);


            //this.SolutionManifest.SaveSettings("test.cgsav");


            //
            this.SolutionFile = SolutionManifest.ProjectManifestFileName;

            //
            this.TemplateProvider = new SQLTableSelection();




        }

        //ProjectManifest SolutionManifest { get; set; }

        private ProjectManifest _SolutionManifest;
        public ProjectManifest SolutionManifest
        {
            get { return _SolutionManifest; }
            set
            {
                _SolutionManifest = value;
                RaisePropertyChanged("SolutionManifest");
            }
        }

        private IProvideTemplateCriteria _TemplateProvider;
        public IProvideTemplateCriteria TemplateProvider
        {
            get { return _TemplateProvider; }
            set
            {
                _TemplateProvider = value;
                RaisePropertyChanged("TemplateProvider");
            }
        }

        private TemplateSelectionListViewModel _TemplateList;
        public TemplateSelectionListViewModel TemplateList
        {
            get { return _TemplateList; }
            set
            {
                _TemplateList = value;
                RaisePropertyChanged("TemplateList");
            }
        }

        private ManifestGroup _SelectedManifestGroup;
        public ManifestGroup SelectedManifestGroup
        {
            get { return _SelectedManifestGroup; }
            set
            {
                _SelectedManifestGroup = value;
                RaisePropertyChanged("SelectedManifestGroup");
            }
        }

        private string _SolutionFile;
        public string SolutionFile
        {
            get { return _SolutionFile; }
            set
            {
                _SolutionFile = value;
                RaisePropertyChanged("SolutionFile");
            }
        }

        private RelayCommand _cmdSelectModule;
        public ICommand cmdSelectModule
        {
            get
            {
                if (_cmdGenerate == null) _cmdSelectModule = new RelayCommand(SelectModule);
                return _cmdSelectModule;
            }
        }

        private void SelectModule()
        {
            int i = 0;

        }

        private RelayCommand _cmdGenerate;
        public ICommand cmdGenerate
        {
            get
            {
                if (_cmdGenerate == null) _cmdGenerate = new RelayCommand(GenerateCode);
                return _cmdGenerate;
            }
        }


        private void GenerateCode()
        {
            var templateContract = this.TemplateProvider;
            List<TemplateViewModel> templatesToRun = this.TemplateList.GetSelectedTemplates();

            foreach (var template in templatesToRun)
            {
                List<KeyValuePair<string, string>> criteria = this.TemplateProvider.GetTemplateCriteria();
                criteria.Add(new KeyValuePair<string, string>("ProcedureName", "testproc"));

                string processedTemplateText = TemplateGenerationEngine.ProcessTemplate(template.FullFileName, criteria);
                //string processedTemplateText = TemplateGenerationEngine.ProcessTemplate(@"C:\Users\jrussell\SkyDrive\Code\Projects\TalTek\CodeGEN\Templates\SQL\selectlist.tt", criteria);

                TextViewer tv = new TextViewer(processedTemplateText);
                tv.Title = template.TemplateName;
                tv.ShowDialog();

            }

            //if (this.OutputToScreen)
            //{
            //    TextViewer tv = new TextViewer(processedTemplateText);
            //    tv.Title = procName;
            //    tv.ShowDialog();
            //}


        }


    }
}
