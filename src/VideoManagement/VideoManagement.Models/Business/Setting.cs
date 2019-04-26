using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class Setting : BaseModel
    {
        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private string key { get; set; }
        private string _value { get; set; }
    }
}
