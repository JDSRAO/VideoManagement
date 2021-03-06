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
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.Helpers;
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
            DataContext = new RecentPathsViewModel();
            recentPaths.MouseDoubleClick += RecentPaths_MouseDoubleClick;
        }

        private void RecentPaths_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedItem = recentPaths.SelectedItem as RecentPath;
        }

        public RecentPath SelectedItem => selectedItem;

        private RecentPath selectedItem { get; set; }

        private void CopyCurrentPath_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var textToCopy = btn.Tag.ToString();
            CopyTool.CopyToClipboard(textToCopy);
        }
    }
}
