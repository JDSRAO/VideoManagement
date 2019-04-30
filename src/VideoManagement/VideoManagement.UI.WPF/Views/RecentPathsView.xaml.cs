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
using VideoManagement.UI.WPF.ViewModels;

namespace VideoManagement.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for RecentPathsView.xaml
    /// </summary>
    public partial class RecentPathsView : UserControl
    {
        public RecentPathsView()
        {
            InitializeComponent();
            DataContext = new RecentPathsModel();
        }
    }
}
