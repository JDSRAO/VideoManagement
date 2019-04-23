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
using VideoManagement.Models;
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
            
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new Forms.FolderBrowserDialog())
            {
                Forms.DialogResult result = dialog.ShowDialog();
                path = dialog.SelectedPath;
            }
            CWD.Text = path;
            Application.Current.Properties.Remove(AppProperties.Path);
            Application.Current.Properties.Remove(AppProperties.Extension);
            Application.Current.Properties.Add(AppProperties.Path, path);
            Application.Current.Properties.Add(AppProperties.Extension, extension);
            videoMgmtService = new VideoMgmtService(path, extension);
            var videos = videoMgmtService.Get();
            Items.ItemsSource = videos;
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems;
            if(addedItems.Count > 0)
            {
                var selectedItem = (Video)addedItems[0];
                Play play = new Play(selectedItem);
                play.Show();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = searchText.Text;
            videoMgmtService = new VideoMgmtService(path, extension);
            var videos = videoMgmtService.Get(query);
            Items.ItemsSource = null;
            Items.ItemsSource = videos;
        }
    }
}
