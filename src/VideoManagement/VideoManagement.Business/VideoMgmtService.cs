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

        public void AddTag(int id, string tag)
        {

        }

        public void AddCategory(int id, string category)
        {

        }

        public List<Video> Search(string query)
        {
            return null;
        }
    }
}
