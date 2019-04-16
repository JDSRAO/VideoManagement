using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Business.Models;

namespace VideoManagement.Business
{
    public class SetupService
    {
        public List<Video> Videos { get; set; }

        public SetupService()
        {
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
    }
}
