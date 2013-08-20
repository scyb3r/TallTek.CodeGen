using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodeGEN.Business.Interfaces;
using GalaSoft.MvvmLight;

namespace CodeGEN.UI.ViewModels
{
    public class SqlServerAuthentication : ViewModelBase, IProvideTemplateCriteria
    {
        public SqlServerAuthentication()
        {
            this.AuthenticationTypes = new ObservableCollection<SqlAuthenticationType>();

            this.AuthenticationTypes.Add(new SqlAuthenticationType() { DisplayName = "Integrated", RequiresPassword = false });
            this.AuthenticationTypes.Add(new SqlAuthenticationType() { DisplayName = "Sql Authentication", RequiresPassword = true });

            this.SelectedAuthenticationType = this.AuthenticationTypes.First();
        }

        #region SQLDBAuth
        private ObservableCollection<SqlAuthenticationType> _AuthenticationTypes;
        public ObservableCollection<SqlAuthenticationType> AuthenticationTypes
        {
            get { return _AuthenticationTypes; }
            set
            {
                _AuthenticationTypes = value;
                RaisePropertyChanged("AuthenticationTypes");
            }
        }

        private SqlAuthenticationType _SelectedAuthenticationType;
        public SqlAuthenticationType SelectedAuthenticationType
        {
            get { return _SelectedAuthenticationType; }
            set
            {
                _SelectedAuthenticationType = value;
                if (!this.SelectedAuthenticationType.RequiresPassword) this.UserName = string.Empty; //clear out UN when using intauth
                RaisePropertyChanged("SelectedAuthenticationType");
                RaisePropertyChanged("DatabaseCollection");
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                //if (settings != null) settings.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
                RaisePropertyChanged("DatabaseCollection");
            }
        }


        public class SqlAuthenticationType
        {
            public string DisplayName { get; set; }
            public bool RequiresPassword { get; set; }
            public bool IsSelected { get; set; }

        }
        #endregion


        #region Properties

        private string _server;

        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                //settings.ServerName = value;

                //if (!settings.SQLServerHistory.Contains(value) && !string.IsNullOrEmpty(value))
                //{
                //    settings.SQLServerHistory.Add(value);
                //}

                RaisePropertyChanged("Server");
                RaisePropertyChanged("DatabaseCollection");
                //RaisePropertyChanged("CanGenerate");
            }
        }

        public Microsoft.SqlServer.Management.Smo.DatabaseCollection DatabaseCollection
        {
            get
            {
                if (string.IsNullOrEmpty(this.Server)) return null;
                if (this.SelectedAuthenticationType.RequiresPassword && string.IsNullOrEmpty(this.Password)) return null;

                try
                {
                    Microsoft.SqlServer.Management.Common.ServerConnection sc;

                    if (this.SelectedAuthenticationType.RequiresPassword)
                        sc = new Microsoft.SqlServer.Management.Common.ServerConnection(this.Server, this.UserName, this.Password);
                    else
                        sc = new Microsoft.SqlServer.Management.Common.ServerConnection(this.Server);

                    sc.ConnectTimeout = 3; //you get 3 seconds - thats it
                    string ver = sc.ServerVersion.BuildNumber.ToString(); //a quick hack i use to force the db connection (to fail if necessary)
                    Microsoft.SqlServer.Management.Smo.Server s = new Microsoft.SqlServer.Management.Smo.Server(sc);
                    return s.Databases;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Failed to connect to SQL Server: {0}", this.Server));
                    return null;
                }
            }
        }

        //private List<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>> _tableCollection;
        //public List<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>> TableCollection
        //{
        //    get
        //    {
        //        if (this.SelectedDatabase == null) return null;
        //        return _tableCollection;
        //    }
        //}

        //private List<SelectableObject<Microsoft.SqlServer.Management.Smo.Column>> _columnCollection;
        //public List<SelectableObject<Microsoft.SqlServer.Management.Smo.Column>> ColumnCollection
        //{
        //    get { return _columnCollection; }
        //    set
        //    {
        //        _columnCollection = value;
        //        RaisePropertyChanged("ColumnCollection");
        //    }
        //}

        public event EventHandler OnDatabaseChanged;


        //private string _templateOutputFormat = "{DatabaseName}.[{SchemaName}].[{TableName}_{TemplateName}]";
        //public string TemplateOutputFormat
        //{
        //    get { return _templateOutputFormat; }
        //    set
        //    {
        //        _templateOutputFormat = value;
        //        settings.OutputNameFormat = value;
        //        RaisePropertyChanged("TemplateOutputFormat");
        //    }
        //}

        //private bool _CheckAll;
        //public bool CheckAll
        //{
        //    get { return _CheckAll; }
        //    set
        //    {
        //        _CheckAll = value;

        //        this._tableCollection.ForEach(o => o.IsSelected = value);
        //        RaisePropertyChanged("CheckAll");
        //        RaisePropertyChanged("TableCollection");
        //        RaisePropertyChanged("CanGenerate");
        //    }
        //}

        private Microsoft.SqlServer.Management.Smo.Database _selectedDatabase;

        public Microsoft.SqlServer.Management.Smo.Database SelectedDatabase
        {
            get { return _selectedDatabase; }
            set
            {
                _selectedDatabase = value;
                if (_selectedDatabase == null) return;

                //_tableCollection = new List<SelectableObject<Microsoft.SqlServer.Management.Smo.Table>>();
                //foreach (var t in _selectedDatabase.Tables)
                //{
                //    SelectableObject<Microsoft.SqlServer.Management.Smo.Table> so = new SelectableObject<Microsoft.SqlServer.Management.Smo.Table>(t);
                //    so.PropertyChange += new SelectableObject<Microsoft.SqlServer.Management.Smo.Table>.PropertyChangeHandler(SelectedTable_PropertyChange);
                //    _tableCollection.Add(so);
                //}

                if (OnDatabaseChanged != null) OnDatabaseChanged(null, System.EventArgs.Empty);

                RaisePropertyChanged("SelectedDatabase");
                //RaisePropertyChanged("TableCollection");
                //RaisePropertyChanged("CanGenerate");
            }
        }

        //private string _templateFolderLocation;
        //public string TemplateFolderLocation
        //{
        //    get { return _templateFolderLocation; }
        //    set
        //    {
        //        _templateFolderLocation = value;
        //        RaisePropertyChanged("TemplateFolderLocation");
        //        RaisePropertyChanged("AvailableTemplates");
        //        settings.TemplateFolderLocation = value;

        //        AvailableTemplates = T4Templates.GetAvailableTemplates(this.TemplateFolderLocation);
        //    }
        //}

        //private bool _outputToScreen = false;
        //public bool OutputToScreen
        //{
        //    get { return _outputToScreen; }
        //    set
        //    {
        //        _outputToScreen = value;

        //        //uncheck the following - they cannot be performed while using screen output
        //        if (_outputToScreen == true)
        //        {
        //            this.ExecuteScriptAfterGeneration = false;
        //            this.AddToBatchFile = false;
        //            this.OutputToFile = false;
        //        }

        //        RaisePropertyChanged("OutputToScreen");

        //        settings.OutputToScreen = value;
        //    }
        //}

        //private bool _outputToFile = true;
        //public bool OutputToFile
        //{
        //    get { return _outputToFile; }
        //    set
        //    {
        //        _outputToFile = value;
        //        RaisePropertyChanged("OutputToFile");
        //        settings.OutputToFile = value;
        //    }
        //}

        //private string _outputFolderLocation;
        //public string OutputFolderLocation
        //{
        //    get { return _outputFolderLocation; }
        //    set
        //    {
        //        _outputFolderLocation = value;
        //        RaisePropertyChanged("OutputFolderLocation");
        //        settings.OutputFolderLocation = value;
        //    }
        //}

        //public List<string> ServerHistory
        //{
        //    get
        //    {
        //        if (settings == null) return null;
        //        return settings.SQLServerHistory;
        //    }
        //}

        //private bool _executeScriptAfterGeneration;
        //public bool ExecuteScriptAfterGeneration
        //{
        //    get { return _executeScriptAfterGeneration; }
        //    set
        //    {
        //        _executeScriptAfterGeneration = value;
        //        RaisePropertyChanged("ExecuteScriptAfterGeneration");
        //        settings.ExecuteScriptAfterGeneration = value;
        //    }
        //}

        //private bool _addToBatchFile;
        //public bool AddToBatchFile
        //{
        //    get { return _addToBatchFile; }
        //    set
        //    {
        //        _addToBatchFile = value;
        //        RaisePropertyChanged("AddToBatchFile");
        //        settings.AddToBatchFile = value;
        //    }
        //}

        //private bool _SelectAll;
        //public bool SelectAll
        //{
        //    get { return _SelectAll; }
        //    set
        //    {
        //        _SelectAll = value;
        //        this.AvailableTemplates.ToList().ForEach(o => o.IsSelected = value);
        //        RaisePropertyChanged("SelectAll");
        //        RaisePropertyChanged("AvailableTemplates");
        //    }
        //}


        //private ObservableCollection<T4TemplateFile> _availableTemplates = new ObservableCollection<T4TemplateFile>();
        //public ObservableCollection<T4TemplateFile> AvailableTemplates
        //{
        //    get { return _availableTemplates; }
        //    set
        //    {
        //        _availableTemplates = value;
        //        RaisePropertyChanged("AvailableTemplates");
        //    }
        //}

        //public bool CanGenerate
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(this.Server) &&
        //                this.SelectedDatabase != null &&
        //            //this.SelectedTable != null &&
        //                this.TableCollection.Where(o => o.IsSelected == true).FirstOrDefault() != null &&
        //                !this.IsGenerating
        //                ;
        //    }
        //}

        //private bool _isGenerating;
        //public bool IsGenerating
        //{
        //    get { return _isGenerating; }
        //    set
        //    {
        //        _isGenerating = value;
        //        RaisePropertyChanged("IsGenerating");
        //        RaisePropertyChanged("CanGenerate");
        //    }
        //}


        //void SelectedTable_PropertyChange(object sender, EventArgs e)
        //{
        //    List<SelectableObject<Microsoft.SqlServer.Management.Smo.Column>> ret = new List<SelectableObject<Microsoft.SqlServer.Management.Smo.Column>>();
        //    Microsoft.SqlServer.Management.Smo.Table t = ((SelectableObject<Microsoft.SqlServer.Management.Smo.Table>)sender).SelectedObject;

        //    foreach (Microsoft.SqlServer.Management.Smo.Column c in t.Columns)
        //    {
        //        ret.Add(new SelectableObject<Microsoft.SqlServer.Management.Smo.Column>(c));
        //    }

        //    this.ColumnCollection = ret;
        //    RaisePropertyChanged("ColumnCollection");
        //    //RaisePropertyChanged("CanGenerate");
        //}

        
        #endregion






        public List<KeyValuePair<string, string>> GetTemplateCriteria()
        {
            var ret = new List<KeyValuePair<string, string>>();

            ret.Add(new KeyValuePair<string, string>("Server", this.Server));
            ret.Add(new KeyValuePair<string, string>("UserName", this.UserName));
            ret.Add(new KeyValuePair<string, string>("Password", this.Password));
            ret.Add(new KeyValuePair<string,string>("Database", this.SelectedDatabase.Name));

            return ret;
        }
    }
}
