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
        public ObservableCollection<Video> Videos
        {
            get => videos;
            set
            {
                videos = value;
                OnPropertyChanged("Videos");
            }
        }

        public Video SelectedVideo
        {
            get => selectedVideo;
            set
            {
                selectedVideo = value;
                OnPropertyChanged("SelectedVideo");
            }
        }

        private ObservableCollection<Video> videos { get; set; }
        private Video selectedVideo { get; set; }
        private VideoMgmtService videoMgmtService { get; set; }

        public VideosViewModel(string path)
        {
            GetVideos(path);
        }

        private void GetVideos(string path)
        {
            videoMgmtService = new VideoMgmtService(path);
            var videos = videoMgmtService.Get().OrderBy(x => x.Name);
            this.videos = new ObservableCollection<Video>(videos);
        }
    }
}
