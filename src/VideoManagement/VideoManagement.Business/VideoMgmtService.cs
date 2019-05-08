using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models.Tables;

namespace VideoManagement.Business
{
    public class VideoMgmtService : BaseManager
    {
        public VideoMgmtService(string path) : base(path)
        {
        }

        public List<AppFile> Get(string query = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                return context.Files.ToList();
            }
            else
            {
                query = query.ToLower();
                return context.Files.Where(x => x.Name.ToLower().Contains(query)
                || x.Tags.Count(a => a.Name.ToLower().Contains(query)) > 1
                || x.Categories.Count(a => a.Name.ToLower().Contains(query)) > 1
                || x.Artists.Count(a => a.Name.ToLower().Contains(query)) > 1)
                .ToList();
            }
        }

        public List<AppFile> Get(Func<AppFile, bool> predicate)
        {
            return context.Files.Where(predicate).ToList();
        }

        public Guid Add(AppFile video)
        {
            var newVideo = context.Files.Add(video);
            context.SaveChanges();
            return newVideo.Entity.ID;
        }

        public void Update(AppFile video)
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

            context.Files.Update(video);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var video = context.Files.Where(x => x.ID == id).FirstOrDefault();
            context.Files.Remove(video);
            context.SaveChanges();
        }

        public List<AppFile> Search(string query)
        {
            return null;
        }
    }
}
