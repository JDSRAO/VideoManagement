using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models.Tables;

namespace VideoManagement.Business
{
    public class BaseManager
    {
        private string DirectoryPath { get; }
        public FileManagerDbContext context { get; }
        private AppMgmtService appMgmtService { get; }

        private string DBFileName = "searchEngine";

        public BaseManager(string path)
        {
            DirectoryPath = path;
            appMgmtService = new AppMgmtService();
            Guid pathId = appMgmtService.ConfigurePreConditionsForPath(path);
            var fileName = $"{pathId}_{DBFileName}.db";
            var connectionString = Path.Combine(Directory.GetCurrentDirectory(), "db", fileName);
            context = new FileManagerDbContext(connectionString);
            if (!DbExists())
            {
                Setup();
            }
        }

        public bool DbExists()
        {
            var setting = context.Settings.Where(x => x.Key.Equals("DbExists")).FirstOrDefault();
            if (setting != null && setting.Value == "false")
            {
                return false;
            }
            return true;
        }

        public string[] GetAllLocalFiles()
        {
            List<string> files = new List<string>();
            var supportedExtensions = appMgmtService.GetSupportedFileExtensions();
            foreach (var extension in supportedExtensions)
            {
                string[] localFiles = Directory.GetFiles(DirectoryPath, $"*{extension.Format}*", SearchOption.AllDirectories);
                files.AddRange(localFiles);
            }
            
            return files.ToArray();
        }

        private void Setup()
        {
            var files = GetAllLocalFiles();
            var videos = new List<Video>();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var isVideo = (appMgmtService.GetFileType(fileName) == FileType.Video ? true : false);
                var thumbNailPath = string.Empty;
                var video = new Video()
                {
                    Name = fileName,
                    Path = file,
                    CreatedOn = File.GetCreationTime(file),
                    IsVideo = isVideo,
                    ThumbnailPath = thumbNailPath
                };
                var category = new Category()
                {
                    Name = video.GetDefaultCategory()
                };
                video.Categories.Add(category);
                videos.Add(video);
            }

            context.Videos.AddRange(videos);
            var setting = context.Settings.Where(x => x.Key == "DbExists").FirstOrDefault();
            setting.Value = "true";
            context.Settings.Update(setting);
            context.SaveChanges();
        }
    }
}
