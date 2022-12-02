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
    public class generaldiscussionController : ApiController
    {
            [HttpPost]
            public IHttpActionResult postgeneraldiscussion(generaldiscussion gd)
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
                        cmd.Parameters.AddWithValue("@userid", gd.newp.userid);
                        cmd.Parameters.AddWithValue("@adminid", gd.newp.adminid);
                        dt = DateTime.Now;
                        cmd.Parameters.AddWithValue("@ptime", dt);
                        cmd.Parameters.AddWithValue("@typeid", gd.newp.typeid);
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
                        cmd.Parameters.AddWithValue("@uid", gd.newp.userid);
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
                    string query3 = "insert into generaldiscussion values (@postid, @details ,@topic)";
                    using (SqlCommand cmd = new SqlCommand(query3, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@postid", postid);
                        cmd.Parameters.AddWithValue("@details", gd.details);
                        cmd.Parameters.AddWithValue("@topic", gd.topic);
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
            public IHttpActionResult getgeneraldiscussions()
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

                List<gdpost> posts = new List<gdpost>();
                string query = "select * from post as p  join generaldiscussion as gd  on (p.postid= gd.postid ) join fusers as u on(p.userid=u.userid)";
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
                                gdpost dum = new gdpost();
                                dum.postid = Convert.ToInt32(sdr["postid"]);
                                dum.details = Convert.ToString(sdr["details"]);
                                dum.topic = Convert.ToString(sdr["topic"]);
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
            public IHttpActionResult getgeneraldiscussion(int id)
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();

                List<generaldiscussion> posts = new List<generaldiscussion>();
                string query = "select * from post as p  join generaldiscussion as gd on (p.postid= gd.postid) where p.userid=@uid";
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
                                generaldiscussion dum = new generaldiscussion();
                                dum.postid = Convert.ToInt32(sdr["postid"]);
                                dum.details = Convert.ToString(sdr["details"]);
                                dum.topic = Convert.ToString(sdr["topic"]);
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
        }
    }

