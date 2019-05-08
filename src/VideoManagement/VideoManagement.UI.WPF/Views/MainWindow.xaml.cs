﻿using System;
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
using VideoManagement.Business;
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.ViewModels;
using VideoManagement.UI.WPF.Views;
using Forms = System.Windows.Forms;

namespace VideoManagement.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoMgmtService videoMgmtService;
        string path = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
            
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder();
        }

        private void MI_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder();
        }

        private void AddEditProperties()
        {
            Application.Current.Properties.Remove(AppProperties.Path);
            Application.Current.Properties.Add(AppProperties.Path, path);
        }

        private void RecentPathsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as RecentPathsView;
            var recentPath = item.SelectedItem as RecentPath;
            path = recentPath.Path;
            AddNewTab(recentPath.Path);
        }

        private void AddNewTab(string path)
        {
            var view = new VideosView(path);
            var tab = new TabItem()
            {
                Header = System.IO.Path.GetFileName(path),
                Content = view
            };
            tabs.Items.Add(tab);
            tabs.SelectedItem = tab;
        }

        private void OpenFolder()
        {
            using (var dialog = new Forms.FolderBrowserDialog())
            {
                Forms.DialogResult result = dialog.ShowDialog();
                if (!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    path = dialog.SelectedPath;
                    AddEditProperties();
                    videoMgmtService = new VideoMgmtService(path);
                    AddNewTab(path);
                }
            }
        }

        private void RefreshPlaylist(string query = null)
        {
            var videos = videoMgmtService.Get(query);
            //Items.ItemsSource = videos.OrderBy(x => x.Name);
        }

        private void MI_ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
