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

        public Guid? GetConnectionStringFor(string path)
        {
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path)).FirstOrDefault();
            if(item != null)
            {
                return item.ID;
            }
            return null;
        }

        public Guid AddPath(string path, string extension)
        {
            var recentPath = new RecentPath()
            {
                Path = path
            };
            var item = dBContext.RecentPaths.Add(recentPath);
            dBContext.SaveChanges();
            return item.Entity.ID;
        }

        public Guid CreatePathIfNotExists(string path, string extension)
        {
            var item = dBContext.RecentPaths.Where(x => x.Path.Equals(path)).FirstOrDefault();
            if (item == null)
            {
                return AddPath(path, extension);
            }
            else
            {
                return item.ID;
            }
        }
    }
}
