using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CodeGEN.UI.ViewModels
{
    //TODO - make this implement inotifypropertychanged without going through MVB - that might obviate the need for messaging
    public class SelectableObject<T> : ViewModelBase
    {
        public delegate void PropertyChangeHandler(object sender, EventArgs e);
        public event PropertyChangeHandler PropertyChange;

        protected void OnPropertyChange(object sender, EventArgs e)
        {
            if (PropertyChange != null) PropertyChange(sender, e);
        }

        public SelectableObject(object selectedObject)
        {
            _selectedObject = (T)selectedObject;
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                    if (PropertyChange != null) PropertyChange(this, null);
                }
            }
        }

        private T _selectedObject;
        public T SelectedObject
        {
            get { return _selectedObject; }
        }

        public override string ToString()
        {
            return this.SelectedObject.ToString();
        }



    }
}
