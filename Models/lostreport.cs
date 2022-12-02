using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class lostreport
    {
        public int postid { get; set; }
        public string details { get; set; }
        public string lostitem { get; set; }
        public post newp;

        public lostreport()
        {
            newp = new post();
        }
    }
}