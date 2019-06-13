using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement.UI.WPF
{
    internal static class AppConfiguration
    {
        public static string ApplicationLogFileName { get; } = GetValueFromConfig("logging:fileName");

        private static string GetValueFromConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
