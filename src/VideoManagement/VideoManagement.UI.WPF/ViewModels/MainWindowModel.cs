using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoManagement.Models.Tables;

namespace VideoManagement.UI.WPF.ViewModels
{
    public class MainWindowModel
    {
        private string path { get; set; }
        private string searchQuery { get; set; }
        private List<Video> videos { get; set; }

        public MainWindowModel()
        {

        }
    }
}
