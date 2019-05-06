using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoManagement.Business;
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.Commands;

namespace VideoManagement.UI.WPF.ViewModels
{
    public class MainWindowModel : BaseViewModel
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

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }

        public ObservableCollection<Video> Videos
        {
            get => videos;
            set
            {
                videos = value;
            }
        }

        private string path { get; set; }
        private string searchQuery { get; set; }
        private ObservableCollection<Video> videos { get; set; }

        string selectedPath = string.Empty;
        string extension = ".mp4";

        public MainWindowModel()
        {

        }

        public ICommand GetVideosCommand
        {
            get
            {
                if (getVideos == null)
                    getVideos = new GetVideos();
                return getVideos;
            }
            set
            {
                getVideos = value;
            }
        }

        private ICommand getVideos;

    }
}
