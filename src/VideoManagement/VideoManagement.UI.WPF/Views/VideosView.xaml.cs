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

namespace VideoManagement.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for VideosView.xaml
    /// </summary>
    public partial class VideosView : UserControl
    {
        public VideosView()
        {
            InitializeComponent();
        }

        public event EventHandler<Video> ItemSelected;

        private void Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = Items.SelectedItem as Video;
            ItemSelected?.Invoke(this, selectedItem);
        }
    }
}
