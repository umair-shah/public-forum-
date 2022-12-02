using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forum_apis.Models
{
    public class usercomment
    {
        public user userdetail;
        public comment comments;
        public usercomment()
        {
            userdetail = new user();
            comments = new comment();
        }
    }
}