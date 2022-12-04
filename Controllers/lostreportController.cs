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
    public class lostreportController : ApiController
    {
        [HttpPost]
        public IHttpActionResult postlostreport(lostreport lp)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            using (SqlConnection con = new SqlConnection(connectionInfo))
            {
                string query1 = "insert into post values (@userid, @adminid ,@ptime ,@typeid)";
                DateTime dt = new DateTime();
                int i = 0;
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@userid", lp.newp.userid);
                    cmd.Parameters.AddWithValue("@adminid", lp.newp.adminid);
                    dt = DateTime.Now;
                    cmd.Parameters.AddWithValue("@ptime", dt);
                    cmd.Parameters.AddWithValue("@typeid", lp.newp.typeid);
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
                string query2 = "select postid from post as p where p.userid = @uid AND p.ptime = @times";
                int postid = new int();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@uid", lp.newp.userid);
                    cmd.Parameters.AddWithValue("@times", dt);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            postid = Convert.ToInt32(sdr["postid"]);
                        }
                    }
                    con.Close();
                    if (postid < 1)
                    {
                        return BadRequest();

                    }
                }
                string query3 = "insert into lostreport values (@postid, @details ,@lostitem)";
                using (SqlCommand cmd = new SqlCommand(query3, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@postid", postid);
                    cmd.Parameters.AddWithValue("@details", lp.details);
                    cmd.Parameters.AddWithValue("@lostitem", lp.lostitem);
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
            return Ok("done");
        }
        [HttpGet]
        public IHttpActionResult getlostreports()
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

            List<lrpost> posts = new List<lrpost>();
            string query = "select * from post as p  join lostreport as lp  on (p.postid= lp.postid ) join fusers as u on(p.userid=u.userid)";
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
                            lrpost dum = new lrpost();
                            dum.postid = Convert.ToInt32(sdr["postid"]);
                            dum.details = Convert.ToString(sdr["details"]);
                            dum.lostitem = Convert.ToString(sdr["lostitem"]);
                            dum.ptime = Convert.ToDateTime(sdr["ptime"]);
                            dum.username = Convert.ToString(sdr["username"]);
                            dum.email = Convert.ToString(sdr["email"]);
                            dum.designation = Convert.ToString(sdr["designation"]);
                            posts.Add(dum);
                        }
                        con.Close();
                    }
                }
            }
            return Ok(posts);
        }
        [HttpGet]
        public IHttpActionResult getuserslostreport(int id)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

            List<lostreport> posts = new List<lostreport>();
            string query = "select * from post as p  join lostreport as lp on (p.postid= lp.postid) where p.userid=@uid";
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
                            lostreport dum = new lostreport();
                            dum.postid = Convert.ToInt32(sdr["postid"]);
                            dum.details = Convert.ToString(sdr["details"]);
                            dum.lostitem = Convert.ToString(sdr["lostitem"]);
                            dum.newp.postid = Convert.ToInt32(sdr["postid"]);
                            dum.newp.adminid = Convert.ToInt32(sdr["adminid"]);
                            dum.newp.ptime = Convert.ToDateTime(sdr["ptime"]);
                            dum.newp.typeid = Convert.ToInt32(sdr["typeid"]);
                            dum.newp.userid = Convert.ToInt32(sdr["userid"]);
                            posts.Add(dum);
                        }
                        con.Close();

                    }
                }
            }
            return Ok(posts);
        }
        [HttpGet]
        public IHttpActionResult getlivelostreports(int id)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

            List<lrpost> posts = new List<lrpost>();
            string query = "select * from post as p  join lostreport as lp  on (p.postid= lp.postid ) join fusers as u on(p.userid=u.userid) where p.postid > @uid";
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
                            lrpost dum = new lrpost();
                            dum.postid = Convert.ToInt32(sdr["postid"]);
                            dum.details = Convert.ToString(sdr["details"]);
                            dum.lostitem = Convert.ToString(sdr["lostitem"]);
                            dum.ptime = Convert.ToDateTime(sdr["ptime"]);
                            dum.username = Convert.ToString(sdr["username"]);
                            dum.email = Convert.ToString(sdr["email"]);
                            dum.designation = Convert.ToString(sdr["designation"]);
                            posts.Add(dum);
                        }
                        con.Close();
                    }
                }
            }
            return Ok(posts);
        }
    }
}
