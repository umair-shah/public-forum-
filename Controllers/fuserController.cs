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

    public class fuserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getuser(int id)
        {
            user users = new user();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            string query = "select * from fusers where userid=@uid ";
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@uid", id);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            users.userid = Convert.ToInt32(sdr["userid"]);
                            users.email = Convert.ToString(sdr["email"]);
                            users.username = Convert.ToString(sdr["username"]);
                            users.designation = Convert.ToString(sdr["designation"]);
                        }
                        con.Close();
                    }
                }
            }
            return Ok(users);
        }
        [HttpPost]
        public IHttpActionResult postnewuser(user newuser)
        {
            //string CS = "Server=.;Database=IPTFORUM;User Id=sa;Password=abc";
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                string query = "insert into fusers values(@username,@email,@upass,@designation)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@username", newuser.username);
                    cmd.Parameters.AddWithValue("@email", newuser.email);
                    cmd.Parameters.AddWithValue("@upass", newuser.password);
                    cmd.Parameters.AddWithValue("@designation", newuser.designation);
                    con.Open();
                    int i = 0;
                    try
                    {
                        i = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        return Ok(e.Message);
                    }
                    if (i > 0)
                    {
                        return Ok("done");
                    }
                }
                return Ok();
            }
        }

    }
}
