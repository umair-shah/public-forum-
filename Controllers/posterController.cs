using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using forum_apis.Models;

namespace forum_apis.Controllers
{
    public class posterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult poster(int id)
        {
            List<user> users = new List<user>();

            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            string query = "";
            if (id == 1)
            {
                query = "select * from fusers where userid in (select p.userid from post as p join generaldiscussion as gd on (p.postid=gd.postid))";
            }
            if (id == 2)
            {
                query = "select * from fusers where userid in (select p.userid from post as p join lostreport as lp on (p.postid=lp.postid))";
            }
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            users.Add(new user
                            {
                                userid = Convert.ToInt32(sdr["userid"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                designation = Convert.ToString(sdr["designation"])
                            });
                        }
                        con.Close();
                    }
                }
            }
            return Ok(users);
        }
    }
}
