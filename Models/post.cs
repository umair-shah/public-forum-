using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class post
    {
        public int postid { get; set; }
        public int userid { get; set; }
        public int adminid { get; set; }
        public DateTime ptime { get; set; }
        public int typeid { get; set; }
    }
}