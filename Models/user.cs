using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class user
    {
        public virtual int userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string designation { get; set; }
    }
}