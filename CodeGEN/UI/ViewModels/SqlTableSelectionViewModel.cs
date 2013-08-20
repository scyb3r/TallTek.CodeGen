using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGEN.Business.Interfaces;
using GalaSoft.MvvmLight;

namespace CodeGEN.UI.ViewModels
{
    public class SqlTableSelectionViewModel : ViewModelBase, IProvideTemplateCriteria
    {
        public SqlTableSelectionViewModel()
        {
            this.SqlAuthentication = new SqlServerAuthentication();
            this.SqlAuthentication.OnDatabaseChanged += SqlAuthentication_OnDatabaseChanged;
        }



        private SqlServerAuthentication _SqlAuthentication;
        public SqlServerAuthentication SqlAuthentication
        {
            get { return _SqlAuthentication; }
            set
            {
                _SqlAuthentication = value;
                RaisePropertyChanged("SqlAuthentication");
            }
        }

        private ObservableCollection<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>> _TableCollection;
        public ObservableCollection<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>> TableCollection
        {
            get { return _TableCollection; }
            set
            {
                _TableCollection = value;
                RaisePropertyChanged("TableCollection");
            }
        }

        private SelectableObject<Microsoft.SqlServer.Management.Smo.Table> _SelectedTable;
        public SelectableObject<Microsoft.SqlServer.Management.Smo.Table> SelectedTable
        {
            get { return _SelectedTable; }
            set
            {
                _SelectedTable = value;
                RaisePropertyChanged("SelectedTable");
            }
        }
        

        void SqlAuthentication_OnDatabaseChanged(object sender, EventArgs e)
        {
            this.TableCollection = new ObservableCollection<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>>();
            foreach (var t in this.SqlAuthentication.SelectedDatabase.Tables)
            {
                SelectableObject<Microsoft.SqlServer.Management.Smo.Table> so = new SelectableObject<Microsoft.SqlServer.Management.Smo.Table>(t);
                //so.PropertyChange += new SelectableObject<Microsoft.SqlServer.Management.Smo.Table>.PropertyChangeHandler(SelectedTable_PropertyChange);
                this.TableCollection.Add(so);
            }


        }


        public List<KeyValuePair<string, string>> GetTemplateCriteria()
        {

            List<KeyValuePair<string, string>> ret = this.SqlAuthentication.GetTemplateCriteria();
            ret.Add(new KeyValuePair<string, string>("Table", this.SelectedTable.SelectedObject.Name));
            ret.Add(new KeyValuePair<string, string>("Schema", this.SelectedTable.SelectedObject.Schema));

            return ret;
        }
    }
}
