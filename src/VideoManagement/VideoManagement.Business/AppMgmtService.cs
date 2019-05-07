using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite.Application;
using VideoManagement.Models.Tables;

namespace VideoManagement.Business
{
    public class AppMgmtService
    {
        private ApplicationDBContext dBContext = new ApplicationDBContext();

        public Guid AddPath(string path)
        {
            var recentPath = new RecentPath()
            {
                Path = path,
                AccessedOn = DateTime.Now
            };
            var item = dBContext.RecentPaths.Add(recentPath);
            dBContext.SaveChanges();
            return item.Entity.ID;
        }

        public Guid ConfigurePreConditionsForPath(string path)
        {
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path)).FirstOrDefault();
            if (item == null)
            {
                return AddPath(path);
            }
            else
            {
                item.AccessedOn = DateTime.Now;
                dBContext.Update(item);
                dBContext.SaveChanges();
                return item.ID;
            }
        }

        public Guid CreatePathIfNotExists(string path, string extension)
        {
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path)).FirstOrDefault();
            if (item == null)
            {
                return AddPath(path);
            }
            else
            {
                return item.ID;
            }
        }

        public List<RecentPath> GetRecentPaths()
        {
            return dBContext.RecentPaths.OrderByDescending(x => x.AccessedOn).ToList();
        }

        public List<FileExtensions> GetSupportedFileExtensions()
        {
            return dBContext.FileExtensions.ToList();
        }

        public FileType GetFileType(string fileNameWithExtension)
        {
            var extension = Path.GetExtension(fileNameWithExtension);
            return dBContext.FileExtensions.Where(x => x.Format.Equals(extension)).FirstOrDefault().Type;
        }
    }
}
