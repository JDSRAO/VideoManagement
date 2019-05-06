using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class RecentPath : BaseModel
    {
        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                OnPropertyChanged("Extension");
            }
        }

        public DateTime AccessedOn
        {
            get => accessedOn;
            set
            {
                accessedOn = value;
                OnPropertyChanged("AccessedOn");
            }
        }

        private string path { get; set; }
        private string extension { get; set; }
        private DateTime accessedOn { get; set; }
    }
}
