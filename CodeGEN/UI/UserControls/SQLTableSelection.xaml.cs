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
using CodeGEN.Business.Interfaces;
using CodeGEN.UI.ViewModels;

namespace CodeGEN.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SQLTableSelection.xaml
    /// </summary>
    public partial class SQLTableSelection : UserControl, IProvideTemplateCriteria
    {
        public SQLTableSelection()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            SqlTableSelectionViewModel vm = new SqlTableSelectionViewModel();
            this.DataContext = vm;


        }





        public List<KeyValuePair<string, string>> GetTemplateCriteria()
        {
            return ((SqlTableSelectionViewModel)this.DataContext).GetTemplateCriteria();

            //throw new NotImplementedException();
        }
    }
}
