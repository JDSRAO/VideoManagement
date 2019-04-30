using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoManagement.Business;
using VideoManagement.Models.Tables;

namespace VideoManagement.UI.WPF.ViewModels
{
    public class RecentPathsModel : BaseViewModel
    {
        private AppMgmtService appMgmtService = new AppMgmtService();

        public RecentPathsModel()
        {
            var recentPathsList = appMgmtService.GetRecentPaths();
            recentPaths = new ObservableCollection<RecentPath>(recentPathsList);
        }

        public ObservableCollection<RecentPath> RecentPaths
        {
            get => recentPaths;
            set
            {
                recentPaths = value;
                OnPropertyChanged("RecentPaths");
            }
        }


        private ObservableCollection<RecentPath> recentPaths { get; set; }
    }
}
