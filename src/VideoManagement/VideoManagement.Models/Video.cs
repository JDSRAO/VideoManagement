using System;
using System.Collections.Generic;

namespace VideoManagement.Models
{
    public class Video
    {
        public int ID { get; set; }

        public string Name => System.IO.Path.GetFileName(Path);

        public string Path { get; set; }

        public List<string> Categories { get; set; }

        public List<string> Tags { get; set; }

        public string GetDefaultCategory()
        {
            var parts = Path.Split('\\');
            return parts[parts.Length - 2];
        }
    }
}
