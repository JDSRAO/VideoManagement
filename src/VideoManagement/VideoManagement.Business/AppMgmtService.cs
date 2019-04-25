using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManagement.DataAccess.SQLite.Application;
using VideoManagement.Models.Application;

namespace VideoManagement.Business
{
    public class AppMgmtService
    {
        private ApplicationDBContext dBContext = new ApplicationDBContext();

        public Guid AddPath(string path, string extension)
        {
            var recentPath = new RecentPath()
            {
                Path = path,
                Extension = extension,
                AccessedOn = DateTime.Now
            };
            var item = dBContext.RecentPaths.Add(recentPath);
            dBContext.SaveChanges();
            return item.Entity.ID;
        }

        public Guid ConfigurePreConditionsForPath(string path, string extension)
        {
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path) && x.Extension.Equals(extension)).FirstOrDefault();
            if (item == null)
            {
                return AddPath(path, extension);
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
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path) && x.Extension.Equals(extension)).FirstOrDefault();
            if (item == null)
            {
                return AddPath(path, extension);
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
    }
}
