using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class comment
    {
        public int postid { get; set; }
        public int userid { get; set; }
        public string cmnt { get; set; }
        public DateTime ctime { get; set; }
    }
}