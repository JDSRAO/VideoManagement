using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Business.Models;

namespace VideoManagement.Business
{
    public class CoreService
    {
        public List<Video> Videos { get; set; }

        public CoreService()
        {

        }

        public List<Video> Search(string query)
        {
            return null;
        }

        public List<Video> GetAllVideo()
        {
            return null;
        }
    }
}
