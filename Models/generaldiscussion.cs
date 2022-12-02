using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class generaldiscussion
    {
        public generaldiscussion()
        {
            newp = new post();
        }
        public int postid { get; set; }
        public string details { get; set; }
        public string topic { get; set; }

        public post newp;
    }
}