﻿using System;
using System.Collections.Generic;
using System.IO;

namespace VideoManagement.Models
{
    public class Video
    {
        public Guid ID { get; set; }

        public string Name => System.IO.Path.GetFileName(Path);

        public string Path { get; set; }

        public List<string> Categories { get; set; }

        public List<string> Tags { get; set; }

        public DateTime CreatedOn => File.GetCreationTime(Path);

        public DateTime? LastAccessTime { get; set; }

        public long Views { get; set; }

        public List<string> Artists { get; set; }

        public bool Favourite { get; set; }

        public string GetDefaultCategory()
        {
            var parts = Path.Split('\\');
            return parts[parts.Length - 2];
        }
    }
}
