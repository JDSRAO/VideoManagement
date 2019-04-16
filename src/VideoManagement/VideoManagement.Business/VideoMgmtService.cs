using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.DataAccess;
using VideoManagement.DataAccess.FileSystem;
using VideoManagement.Models;

namespace VideoManagement.Business
{
    public class VideoMgmtService
    {
        public List<Video> Videos { get; set; }

        private IVideoMgmtRepository<Video> repository;

        public VideoMgmtService(string path)
        {
            repository = new FileRepository<Video>(path);
            Videos = GetAllVideo();
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
