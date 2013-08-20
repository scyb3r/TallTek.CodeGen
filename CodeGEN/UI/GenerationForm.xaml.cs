using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeGEN.UI.ViewModels;

namespace CodeGEN.UI
{
    /// <summary>
    /// Interaction logic for GenerationForm.xaml
    /// </summary>
    public partial class GenerationForm : UserControl
    {
        public GenerationForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            GenerationFormViewModel vm = new GenerationFormViewModel();
            this.DataContext = vm;

            //vm.TemplateList.LoadTemplatesFromFolder(@"C:\Users\jrussell\SkyDrive\Code\Projects\TalTek\CodeGEN\Templates\");
            

        }
    }
}
