using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Path => (string) Current.Properties[AppProperties.Path];
        public static string Exntension => (string) Current.Properties[AppProperties.Extension];
    }
}
