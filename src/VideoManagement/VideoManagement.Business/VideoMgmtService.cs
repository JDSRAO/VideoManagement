using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models;

namespace VideoManagement.Business
{
    public class VideoMgmtService : BaseManager
    {
        public VideoMgmtService(string path, string fileExtension) : base(path, fileExtension)
        {
        }

        public List<Video> Get(string query = null)
        {
            if (string.IsNullOrEmpty(query))
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

        public Guid Add(Video video)
        {
            var newVideo = context.Videos.Add(video);
            context.SaveChanges();
            return newVideo.Entity.ID;
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

        public void Delete(Guid id)
        {
            var video = context.Videos.Where(x => x.ID == id).FirstOrDefault();
            context.Videos.Remove(video);
            context.SaveChanges();
        }

        public List<Video> Search(string query)
        {
            return null;
        }
    }
}
