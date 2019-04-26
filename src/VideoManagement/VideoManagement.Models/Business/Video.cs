using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class Video
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
    }
}
