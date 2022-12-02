using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class usergeneraldiscussion
    {
        public user nuser;
        public lostreport nlostreport;
        usergeneraldiscussion()
        {
            nuser = new user();
            nlostreport = new lostreport();
        }
    }
}