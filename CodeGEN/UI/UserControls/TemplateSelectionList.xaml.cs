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

namespace CodeGEN.UI.UserControls
{
    /// <summary>
    /// Interaction logic for TemplateSelectionList.xaml
    /// </summary>
    public partial class TemplateSelectionList : UserControl
    {
        public TemplateSelectionList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //TemplateSelectionListViewModel vm = new TemplateSelectionListViewModel();
            //this.DataContext = vm;

        }

    }
}
