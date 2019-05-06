using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Tables
{
    public class RecentPath
    {
        public Guid ID { get; set; }
        public string Path { get; set; }
        public DateTime AccessedOn { get; set; }
    }
}
