using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.DataAccess
{
    public interface IVideoMgmtRepository<T> where T : class
    {
        void Add(T[] videos);

        void Add(T video);

        void Update(T video);

        void Delete(T video);
    }
}
