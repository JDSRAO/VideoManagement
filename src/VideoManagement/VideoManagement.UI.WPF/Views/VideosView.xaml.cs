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
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.ViewModels;

namespace VideoManagement.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for VideosView.xaml
    /// </summary>
    public partial class VideosView : UserControl
    {
        private string currentPath { get; }

        private AppFile currentlySelectedItem { get; set; }

        public VideosView(string path)
        {
            InitializeComponent();
            currentPath = path;
            RefreshPlaylist();
        }

        private void Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            currentlySelectedItem = Items.SelectedItem as AppFile;
            AddEditProperties();
            PlayWindow playWindow = new PlayWindow(currentlySelectedItem);
            playWindow.Loaded += PlayWindow_Loaded;
            playWindow.Show();
        }

        private void PlayWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPlaylist();
        }

        private void AddEditProperties()
        {
            Application.Current.Properties.Remove(AppProperties.Path);
            Application.Current.Properties.Add(AppProperties.Path, currentPath);
        }

        private void RefreshPlaylist()
        {
            DataContext = new VideosViewModel(currentPath);
            if (currentlySelectedItem != null)
            {
                Items.SelectedItem = currentlySelectedItem;
            }
        }
    }
}
