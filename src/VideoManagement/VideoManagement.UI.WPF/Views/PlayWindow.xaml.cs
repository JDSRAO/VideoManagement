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
using VideoManagement.Models.Tables;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        private VideoMgmtService videoMgmtService { get; }
        private Video Video { get; }

        private DispatcherTimer timer;

        public PlayWindow(Video video)
        {
            InitializeComponent();
            videoMgmtService = new VideoMgmtService(App.Path, App.Exntension);
            Video = video;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ShowPosition();
        }

        private void ShowPosition()
        {
            if (player.Source != null)
            {
                if (player.NaturalDuration.HasTimeSpan)
                {
                    sbarPosition.Value = player.Position.TotalSeconds;
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
            EnableButtons(true);
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            EnableButtons(false);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            sbarPosition.Value = 0;
            EnableButtons(false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Video.Views = ++Video.Views;
            Video.LastAccessTime = DateTime.Now;
            videoMgmtService.Update(Video);
            Title = Video.Name;
            player.Source = new Uri(Video.Path, uriKind: UriKind.Absolute);
            player.Play();
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            EnableButtons(true);
            volumePosition.Value = 50;
        }

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            sbarPosition.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            sbarPosition.Visibility = Visibility.Visible;
        }

        private void EnableButtons(bool is_playing)
        {
            if (is_playing)
            {
                btnPlay.Visibility = Visibility.Collapsed;
                btnPause.Visibility = Visibility.Visible;
                btnPlay.IsEnabled = false;
                btnPause.IsEnabled = true;
                btnPlay.Opacity = 0.5;
                btnPause.Opacity = 1.0;
            }
            else
            {
                btnPlay.IsEnabled = true;
                btnPause.IsEnabled = false;
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Collapsed;
                btnPlay.Opacity = 1.0;
                btnPause.Opacity = 0.5;
            }
            timer.IsEnabled = is_playing;
        }

        private void SbarPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(e.NewValue);
            player.Position = timespan;
            ShowPosition();
        }

        private void VolumePosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = e.NewValue;
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            player.Stop();
            sbarPosition.Minimum = 0;
        }
    }
}
