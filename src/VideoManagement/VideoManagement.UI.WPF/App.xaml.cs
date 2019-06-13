﻿using log4net;
using Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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

        private static ILog Logger = AppLogger.GetLogger<App>();

        public App()
        {
            AppLogger.ConfigureFileAppender(AppConfiguration.ApplicationLogFileName, true);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Info("App started");
            base.OnStartup(e);
            SetupExceptionHandling();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.Info("App exited");
            base.OnExit(e);
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            //DispatcherUnhandledException += (s, e) =>
            //    LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException", e);

            //TaskScheduler.UnobservedTaskException += (s, e) =>
            //    LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
        }

        private void LogUnhandledException(Exception exception, string source, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e = null)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Unhandled exception ({source})");
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message.AppendFormat("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
                message.AppendLine();
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception in LogUnhandledException : {ex.Message}", ex);
            }
            finally
            {
                message.AppendLine($"Message : {exception.Message}");
                Logger.Error(message.ToString(), exception);
            }
        }
    }
}
