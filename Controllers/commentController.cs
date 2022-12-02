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
    public class commentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getcomment(int id)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            List<usercomment> uc = new List<usercomment>();
            string query = "select * from comments as c  join fusers as u  on (c.userid= u.userid) where c.postid=@id";
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        usercomment dum = null;
                        while (sdr.Read())
                        {
                            dum = new usercomment();

                            dum.comments = (new comment
                            {
                                postid = Convert.ToInt32(sdr["postid"]),
                                userid = Convert.ToInt32(sdr["userid"]),
                                cmnt = Convert.ToString(sdr["cmnt"]),
                                ctime = Convert.ToDateTime(sdr["ctime"])
                            });
                            dum.userdetail = (new user
                            {
                                userid = Convert.ToInt32(sdr["userid"]),
                                username = Convert.ToString(sdr["username"]),
                                designation = Convert.ToString(sdr["designation"]),
                                email = Convert.ToString(sdr["email"])
                            });

                            uc.Add(dum);
                            dum = null;
                        }
                        con.Close();
                    }
                }
            }
            return Ok(uc);
        }
        [HttpPost]
        public IHttpActionResult postcomment(comment c)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                string query1 = "insert into comments values (@postid ,@userid ,@cmnt,@ctime)";
                DateTime dt = new DateTime();
                int i = 0;
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@userid", c.userid);
                    cmd.Parameters.AddWithValue("@postid", c.postid);
                    dt = DateTime.Now;
                    cmd.Parameters.AddWithValue("@ctime", dt);
                    cmd.Parameters.AddWithValue("@cmnt", c.cmnt);
                    con.Open();
                    try
                    {

                        i = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);

                    }
                }
            }
            return Ok();
        }
    }
}
