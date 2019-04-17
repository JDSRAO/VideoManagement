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
            return null;
        }

        public void AddTag()
        {

        }

        public void AddCategory()
        {

        }

        public List<Video> Search(string query)
        {
            return null;
        }
    }
}
