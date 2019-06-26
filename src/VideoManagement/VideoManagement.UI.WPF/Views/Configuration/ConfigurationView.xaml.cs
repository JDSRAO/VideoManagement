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

namespace VideoManagement.UI.WPF.Views.Configuration
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            InitializeComponent();
            DataContext = new ConfigurationViewModel();
        }

        private void AddExtention_Click(object sender, RoutedEventArgs e)
        {
            var context = (ConfigurationViewModel)DataContext;
            context.AddNewExtensionCommand.Execute(null);
        }

        private void IsExtensionEnabled_Click(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var id = cb.Tag.ToString();
            var context = (ConfigurationViewModel)DataContext;
            context.ToggleExtensionStatusCommand.Execute(id);
        }
    }
}
