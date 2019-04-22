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

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoMgmtService videoMgmtService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var path = @"C:\Users\SrinivasRao\Music\Phone";
            var extension = ".mp3";
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
                Window playPage = new Window();
                playPage.Content = new MediaElement()
                {
                    Source = new Uri(selectedItem.Path, UriKind.Absolute)
                };

                playPage.Show();
            }
        }
    }
}
