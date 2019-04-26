using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class Setting : BaseModel
    {
        public string Key { get; set; }

        public string Value{ get; set; }
    }
}
