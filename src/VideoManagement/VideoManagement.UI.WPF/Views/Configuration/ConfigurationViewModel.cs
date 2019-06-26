using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoManagement.Business;
using VideoManagement.Models.Tables;
using VideoManagement.UI.WPF.Commands;
using VideoManagement.UI.WPF.ViewModels;

namespace VideoManagement.UI.WPF.Views.Configuration
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public ICommand AddNewExtensionCommand { get; set; }

        public ObservableCollection<FileExtensions> FileExtensions
        {
            get => fileExtensions;
            set
            {
                fileExtensions = value;
                OnPropertyChanged("FileExtensions");
            }
        }

        public ObservableCollection<FileType> MediaTypes
        {
            get => mediaTypes;
            set
            {
                mediaTypes = value;
                OnPropertyChanged("MediaTypes");
            }
        }

        public string Extention
        {
            get => extention;
            set
            {
                extention = value;
                OnPropertyChanged("Extention");
            }
        }

        public FileType Filetype
        {
            get => filetype;
            set
            {
                filetype = value;
                OnPropertyChanged("Extention");
            }
        }

        private ObservableCollection<FileExtensions> fileExtensions { get; set; }
        private ObservableCollection<FileType> mediaTypes { get; set; }
        private AppMgmtService appMgmtService { get; set; }
        private string extention { get; set; }
        private FileType filetype { get; set; }

        public ConfigurationViewModel()
        {
            appMgmtService = new AppMgmtService();
            AddNewExtensionCommand = new RelayCommand(OnAddNewExtension);
            Setup();
        }

        private void Setup()
        {
            GetExtensions();
            var mediaTypes = appMgmtService.GetMediaTypes();
            SetMediaTypes(mediaTypes);
        }

        private void GetExtensions()
        {
            var supportedExtensions = appMgmtService.GetSupportedFileExtensions();
            SetExtenstions(supportedExtensions);
        }

        private void OnAddNewExtension(object obj)
        {
            appMgmtService.AddNewFileExtension(extention, filetype);
            GetExtensions();
        }

        private void SetExtenstions(List<FileExtensions> fileExtensions)
        {
            this.fileExtensions = new ObservableCollection<FileExtensions>(fileExtensions);
            OnPropertyChanged("FileExtensions");
        }

        private void SetMediaTypes(List<FileType> fileTypes)
        {
            mediaTypes = new ObservableCollection<FileType>(fileTypes);
            OnPropertyChanged("MediaTypes");
        }
    }
}
