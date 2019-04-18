using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoManagement.DataAccess;
using VideoManagement.DataAccess.FileSystem;
using VideoManagement.Models;

namespace VideoManagement.Business
{
    public class VideoMgmtService
    {
        private IVideoMgmtRepository repository;

        public VideoMgmtService(string path, string fileExtension)
        {
            repository = new FileRepository(path, fileExtension);
        }

        public List<Video> GetAllVideo()
        {
            return repository.Get();
        }

        public Guid Add(Video video)
        {
            return repository.Add(video);
        }

        public void Update(Video video)
        {
            repository.Update(video);
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
        }

        public List<Video> Search(string query)
        {
            return null;
        }
    }
}
