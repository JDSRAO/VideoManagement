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
using VideoManagement.Models;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        public Play(Video video)
        {
            InitializeComponent();
            Title = video.Name;
            player.Source = new Uri(video.Path, uriKind: UriKind.Absolute);
        }
    }
}
