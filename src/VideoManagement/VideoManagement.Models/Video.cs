using System;
using System.Collections.Generic;

namespace VideoManagement.Models
{
    public class Video
    {
        public string Name => System.IO.Path.GetFileName(Path);

        public string Path { get; set; }

        public List<string> Categogies { get; set; }

        public List<string> Tags { get; set; }
    }
}
