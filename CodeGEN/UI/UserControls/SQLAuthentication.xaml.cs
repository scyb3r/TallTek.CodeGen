using CodeGEN.UI.ViewModels;
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

namespace CodeGEN.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SQLAuthentication.xaml
    /// </summary>
    public partial class SQLAuthentication : UserControl
    {
        public SQLAuthentication()
        {
            InitializeComponent();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)e.OriginalSource;
            (this.DataContext as SqlServerAuthentication).Password = pb.Password;
        }

        private void cboDatabase_GotFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
