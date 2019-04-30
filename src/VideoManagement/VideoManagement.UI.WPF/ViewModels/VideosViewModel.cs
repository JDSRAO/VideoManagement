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
    public class VideosViewModel : BaseViewModel
    {
        private VideoMgmtService videoMgmtService;

        public VideosViewModel(string path, string fileExtension)
        {
            var videos = videoMgmtService.Get();
            this.videos = new ObservableCollection<Video>(videos);
        }

        public ObservableCollection<Video> Videos
        {
            get => videos;
            set
            {
                videos = value;
                OnPropertyChanged("Videos");
            }
        }


        private ObservableCollection<Video> videos { get; set; }
    }
}
