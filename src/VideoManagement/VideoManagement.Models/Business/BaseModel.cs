using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class BaseModel : INotifyPropertyChanged
    {
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Guid id;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
