using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Tables
{
    public class Artist : BaseModel
    {
        public string Name { get; set; }
        public Guid AppFileId { get; set; }
        public AppFile AppFile { get; set; }
    }
}
