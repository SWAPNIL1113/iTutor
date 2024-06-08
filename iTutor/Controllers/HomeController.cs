using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTutor.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace iTutor.Controllers
{
    public class HomeController : Controller
    {
        string connectionstring = @"Data Source=SURBHI;Initial Catalog=iTutor;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }

     

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Services()
        {
            List<Syllabus> viewcustomerdetails = new List<Syllabus>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewstudy", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                viewcustomerdetails.Add(
                     new Syllabus
                     {
                         id = Convert.ToInt32(dr["id"]),
                         std = Convert.ToString(dr["std"]),
                         sub = Convert.ToString(dr["sub"]),
                         topic = Convert.ToString(dr["topic"]),
                         description = Convert.ToString(dr["description"]),
                         image = Convert.ToString(dr["image"]),
                         part1 = Convert.ToString(dr["part1"]),
                         part2 = Convert.ToString(dr["part2"]),
                        

                     }
                    );
            }
            cn.Close();
            return View(viewcustomerdetails);

        }



        public ActionResult Student_Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Student_Register(Studendt_Register std, HttpPostedFileBase profile)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                string _Filename = Path.GetFileName(profile.FileName);
                string _Path = Path.Combine(Server.MapPath("~/UploadFile"), _Filename);
                profile.SaveAs(_Path);

                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("Insert into TblRegister values (@firstname,@lastname,@address,@state,@phone,@gender,@profile,@email,@password)", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstname", std.firstname);
                cmd.Parameters.AddWithValue("@lastname", std.lastname);
                cmd.Parameters.AddWithValue("@address", std.address);
                cmd.Parameters.AddWithValue("@state", std.state);
                cmd.Parameters.AddWithValue("@phone", std.phone);
                cmd.Parameters.AddWithValue("@gender", std.gender);
                cmd.Parameters.AddWithValue("@profile", _Filename);
                cmd.Parameters.AddWithValue("@email", std.email);
                cmd.Parameters.AddWithValue("@password", std.password);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return View("Student_Login");
            }

        }

        public ActionResult Tutor_Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Tutor_Register(tutor_register std)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("T_Register", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstname", std.firstname);
                cmd.Parameters.AddWithValue("@lastname", std.lastname);
                cmd.Parameters.AddWithValue("@gender", std.gender);
                cmd.Parameters.AddWithValue("@dob", std.dob);
                cmd.Parameters.AddWithValue("@state", std.state);
                cmd.Parameters.AddWithValue("@city", std.city);
                cmd.Parameters.AddWithValue("@address", std.address);
                cmd.Parameters.AddWithValue("@qualification", std.qualification);
                cmd.Parameters.AddWithValue("@experience", std.experience);
                cmd.Parameters.AddWithValue("@phone", std.phone);
                cmd.Parameters.AddWithValue("@email", std.email);
                cmd.Parameters.AddWithValue("@password", std.password);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return View("Student_Login");
            }

        }

        public ActionResult Student_Login()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Student_Login(Models.Login log)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (log.usertype == "Admin")
            {
                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("AdminLogin", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", log.email);
                cmd.Parameters.AddWithValue("@password", log.password);
                cn.Open();
                Object UserId = cmd.ExecuteScalar();
                if (UserId != null)
                {
                    ViewBag.login = "Login Successfully";
                    Session["UserId"] = UserId;

                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    ViewBag.login = "Login Failed";
                    return View("");
                }

            }
            else if (log.usertype == "Student")
            {
                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("StudentLogin", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", log.email);
                cmd.Parameters.AddWithValue("@password", log.password);
                cn.Open();
                Object UserId = cmd.ExecuteScalar();
                if (UserId != null)
                {
                    ViewBag.login = "Login Successfully";
                    Session["UserId"] = UserId;

                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    ViewBag.login = "Login Failed";
                    return View();
                }
            }
            else if (log.usertype == "Tutor")
            {
                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("TutorLogin", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", log.email);
                cmd.Parameters.AddWithValue("@password", log.password);
                cn.Open();
                Object UserId = cmd.ExecuteScalar();
                if (UserId != null)
                {
                    ViewBag.login = "Login Successfully";
                    Session["UserId"] = UserId;

                    return RedirectToAction("Index", "Tutor");
                }
                else
                {
                    ViewBag.login = "Login Failed";
                    return View();
                }

            }
                    return View();
            
        }
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["email"] = null;
            return RedirectToAction("Student_Login");
        }

        }
}
