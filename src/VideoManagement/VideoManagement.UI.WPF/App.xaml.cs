using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VideoManagement.Business;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Path => (string) Current.Properties[AppProperties.Path];

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppMgmtService appMgmtService = new AppMgmtService();
        }
    }
}
