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
using System.Windows.Shapes;
using System.Windows.Threading;
using VideoManagement.Business;
using VideoManagement.Models;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        private VideoMgmtService videoMgmtService { get; }

        public Play(Video video)
        {
            InitializeComponent();
            videoMgmtService = new VideoMgmtService(App.Path, App.Exntension);
            video.Views = ++video.Views;
            video.LastAccessTime = DateTime.Now;
            videoMgmtService.Update(video);
            Title = video.Name;
            player.Source = new Uri(video.Path, uriKind: UriKind.Absolute);
            player.Play();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (player.Source != null)
            {
                if (player.NaturalDuration.HasTimeSpan)
                {
                    lblStatus.Content = String.Format("{0} / {1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
            }
            else
            {
                lblStatus.Content = "No file selected...";
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
    }
}
