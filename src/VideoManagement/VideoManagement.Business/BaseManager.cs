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
        private string FilesToConsider { get; }
        public FileManagerDbContext context { get; }
        private AppMgmtService appMgmtService { get; }

        private string DBFileName = "searchEngine.db";

        public BaseManager(string path, string fileExtension)
        {
            FilesToConsider = fileExtension;
            DirectoryPath = path;
            appMgmtService = new AppMgmtService();
            Guid pathId = appMgmtService.ConfigurePreConditionsForPath(path, fileExtension);
            var fileName = $"{pathId}_{DBFileName}";
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
            string[] files = Directory.GetFiles(DirectoryPath, $"*{FilesToConsider}*", SearchOption.AllDirectories);
            return files;
        }

        private void Setup()
        {
            var files = GetAllLocalFiles();
            var videos = new List<Video>();
            foreach (var file in files)
            {
                var video = new Video()
                {
                    Path = file
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
