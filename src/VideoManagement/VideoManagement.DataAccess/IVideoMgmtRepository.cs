using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VideoManagement.Models;

namespace VideoManagement.DataAccess
{
    public interface IVideoMgmtRepository
    {
        bool DbExists();

        void Add(List<Video> videos);

        void Add(Video video);

        void Update(Video video);

        void Delete(Video video);

        Video Get(int id);

        List<Video> Get(string query = null);

        List<Video> Get(Func<Video, bool> predicate);
    }
}
