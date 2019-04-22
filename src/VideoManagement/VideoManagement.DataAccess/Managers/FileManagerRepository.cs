using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models;

namespace VideoManagement.DataAccess.Managers
{
    public class VideoManager
    {
        private string DirectoryPath { get; }
        private string FilesToConsider { get; }
        public FileManagerDbContext context { get; }

        private string DBFileName = "searchEngine.db";

        public VideoManager(string path, string fileExtension)
        {
            var fileName = $"{fileExtension}_{DBFileName}";
            FilesToConsider = fileExtension;
            DirectoryPath = path;
            var pathWithFileName = Path.Combine(path, fileName);
            context = new FileManagerDbContext(pathWithFileName);
            if(!DbExists())
            {
                Setup();
            }
        }

        public void Add(List<Video> videos)
        {
            context.Videos.AddRange(videos);
            context.SaveChanges();
        }

        public Guid Add(Video video)
        {
            var newVideo = context.Videos.Add(video);
            context.SaveChanges();
            return newVideo.Entity.ID;
        }

        public bool DbExists()
        {
            var setting = context.Settings.Where(x => x.Key.Equals("DbExists")).FirstOrDefault();
            if(setting != null && setting.Value == "false")
            {
                return false;
            }
            return true;
        }

        public void Delete(Guid id)
        {
            var video = context.Videos.Where(x => x.ID == id).FirstOrDefault();
            context.Videos.Remove(video);
            context.SaveChanges();
        }

        public Video Get(Guid id)
        {
            return context.Videos.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<Video> Get(string query = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                return context.Videos.ToList();
            }
            else
            {
                return context.Videos.Where(x => x.Name.Contains(query) 
                || x.Tags.Count(a => a.Name.Contains(query)) > 1 
                || x.Categories.Count(a => a.Name.Contains(query)) > 1
                || x.Artists.Count(a => a.Name.Contains(query)) > 1)
                .ToList();
            }
        }

        public List<Video> Get(Func<Video, bool> predicate)
        {
            return context.Videos.Where(predicate).ToList();
        }

        public void Setup()
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

        public void Update(Video video)
        {
            //var videoToUpdate = context.Videos.Where(x => x.ID == video.ID).FirstOrDefault();
            //var flags = BindingFlags.Public | BindingFlags.Instance;
            //var videoProperties = video.GetType().GetProperties(flags);
            //var videoToUpdateProperties = videoToUpdate.GetType().GetProperties(flags);
            //foreach (var property in videoProperties)
            //{
            //    if (property.GetValue(video) != null && property.CanWrite)
            //    {
            //        var dataProperty = videoToUpdateProperties.FirstOrDefault(x => x.Name.Equals(property.Name));
            //        dataProperty.SetValue(videoToUpdate, property.GetValue(video));
            //    }
            //}

            context.Videos.Update(video);
            context.SaveChanges();
        }

        public string[] GetAllLocalFiles()
        {
            string[] files = Directory.GetFiles(DirectoryPath, $"*{FilesToConsider}*", SearchOption.AllDirectories);
            return files;
        }
    }
}
