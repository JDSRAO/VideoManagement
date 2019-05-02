using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoManagement.Business;
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.ViewModels;
using VideoManagement.UI.WPF.Views;
using Forms = System.Windows.Forms;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoMgmtService videoMgmtService;
        string path = string.Empty; 
        string extension = ".mp4";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
            
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new Forms.FolderBrowserDialog())
            {
                Forms.DialogResult result = dialog.ShowDialog();
                if(!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    path = dialog.SelectedPath;
                    CWD.Text = path;
                    Application.Current.Properties.Remove(AppProperties.Path);
                    Application.Current.Properties.Remove(AppProperties.Extension);
                    Application.Current.Properties.Add(AppProperties.Path, path);
                    Application.Current.Properties.Add(AppProperties.Extension, extension);
                    videoMgmtService = new VideoMgmtService(path, extension);
                    AddNewTab();
                    RefreshPlaylist();
                    videosView.DataContext = new VideosViewModel(path, extension);
                }
            }
        }

        private void AddNewTab()
        {
            var dataContext = new VideosViewModel(path, extension);
            var view = new VideosView();
            view.DataContext = dataContext;
            var tab = new TabItem()
            {
                Header = path,
                Content = view
            };
            tabs.Items.Add(tab);
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems;
            if(addedItems.Count > 0)
            {
                var selectedItem = (Video)addedItems[0];
                PlayWindow play = new PlayWindow(selectedItem);
                play.Loaded += Play_Loaded;
                play.Show();
            }
        }

        private void Play_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPlaylist();
        }

        private void RefreshPlaylist(string query = null)
        {
            var videos = videoMgmtService.Get(query);
            //Items.ItemsSource = videos.OrderBy(x => x.Name);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = searchText.Text;
            videoMgmtService = new VideoMgmtService(path, extension);
            RefreshPlaylist(query);
        }
    }
}
