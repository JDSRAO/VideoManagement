using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class Artist : BaseModel
    {
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string name { get; set; }
    }
}
