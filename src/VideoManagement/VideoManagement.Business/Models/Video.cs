using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Business.Models
{
    public class Video
    {
        public string Name => System.IO.Path.GetFileName(Path);

        public string Path { get; set; }

        public List<string> Categogies { get; set; }

        public List<string> Tags { get; set; }
    }
}
