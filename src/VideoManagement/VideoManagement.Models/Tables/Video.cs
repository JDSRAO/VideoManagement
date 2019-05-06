using System;
using System.Collections.Generic;
using System.IO;

namespace VideoManagement.Models.Tables
{
    public class Video : BaseModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public List<Category> Categories { get; set; }

        public List<Tag> Tags { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastAccessTime { get; set; }

        public long Views { get; set; }

        public List<Artist> Artists { get; set; }

        public bool Favourite { get; set; }

        public string GetDefaultCategory()
        {
            var parts = Path.Split('\\');
            return parts[parts.Length - 2];
        }

        public Video()
        {
            Categories = new List<Category>();
            Tags = new List<Tag>();
            Artists = new List<Artist>();
        }
    }
}
