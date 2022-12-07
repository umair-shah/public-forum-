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
    public class authfuserController : ApiController
    {
        public struct loginuser
        {
            public string email;
            public string password;
        }
        [HttpPost]
        public IHttpActionResult authloginuser(loginuser user)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            int userid = -1;
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                string query = "select userid from fusers as u where u.email=@em AND u.upass= @pass";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@em", user.email);
                    cmd.Parameters.AddWithValue("@pass", user.password);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            userid = Convert.ToInt32(sdr["userid"]);
                        }
                    }
                }
                con.Close();
            }
            if (userid > -1)
            {
                return Ok(userid);
            }
            else
            {
                return Ok("incorrect email or password");
            }
        }
    }
}
