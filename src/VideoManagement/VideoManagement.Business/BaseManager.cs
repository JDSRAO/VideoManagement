using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models;

namespace VideoManagement.Business
{
    public class BaseManager
    {
        private string DirectoryPath { get; }
        private string FilesToConsider { get; }
        public FileManagerDbContext context { get; }

        private string DBFileName = "searchEngine.db";

        public BaseManager(string path, string fileExtension)
        {
            var fileName = $"{fileExtension}_{DBFileName}";
            FilesToConsider = fileExtension;
            DirectoryPath = path;
            var pathWithFileName = Path.Combine(path, fileName);
            context = new FileManagerDbContext(pathWithFileName);
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
                    ID = Guid.NewGuid(),
                    Path = file
                };
                var category = new Category()
                {
                    ID = Guid.NewGuid(),
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
