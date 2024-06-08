using iTutor.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTutor.Controllers
{
    public class StudentController : Controller
    {
        string connectionstring = @"Data Source=SURBHI;Initial Catalog=iTutor;Integrated Security=True";


        // GET: Student
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            return View();
        }

        public ActionResult Study_Material()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<Syllabus> viewcustomerdetails = new List<Syllabus>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewstudymaterial", cn);
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

        public ActionResult Tutors()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<tutor_register> viewcustomerdetails = new List<tutor_register>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewtutor1", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                viewcustomerdetails.Add(
                     new tutor_register
                     {
                         id = Convert.ToInt32(dr["id"]),
                         firstname = Convert.ToString(dr["firstname"]),
                         lastname = Convert.ToString(dr["lastname"]),
                         gender = Convert.ToString(dr["gender"]),
                         dob = Convert.ToString(dr["dob"]),
                         state = Convert.ToString(dr["state"]),
                         city = Convert.ToString(dr["city"]),
                         address = Convert.ToString(dr["address"]),
                         qualification = Convert.ToString(dr["qualification"]),
                         experience = Convert.ToString(dr["experience"]),
                         phone = Convert.ToString(dr["phone"]),
                         email = Convert.ToString(dr["email"]),
                         password = Convert.ToString(dr["password"])

                     }
                    );
            }


            cn.Close();
            return View(viewcustomerdetails);

        }

        public ActionResult Profile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            Studendt_Register data = new Studendt_Register();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblRegister where id="+ Session["UserId"], cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                data.id = Convert.ToInt32(dr["id"]);
                data.firstname = Convert.ToString(dr["firstname"]);
                data.lastname = Convert.ToString(dr["lastname"]);
                data.address = Convert.ToString(dr["address"]);
                data.state = Convert.ToString(dr["state"]);
                data.phone = Convert.ToString(dr["phone"]);
                data.gender = Convert.ToString(dr["gender"]);
                data.profile = "~/UploadFile/" + Convert.ToString(dr["profile"]);
                data.email = Convert.ToString(dr["email"]);
                data.password = Convert.ToString(dr["password"]);

              
            }


            cn.Close();
            return View(data);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["email"] = null;
            return RedirectToAction("Student_Login","Home");
        }

        public ActionResult Details(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            //List<Syllabus> viewcustomerdetails = new List<Syllabus>();
            Syllabus syllabus = new Syllabus();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblSyllabus where id=" + id, cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {


                syllabus.id = Convert.ToInt32(dr["id"]);
                syllabus.std = Convert.ToString(dr["std"]);
                syllabus.sub = Convert.ToString(dr["sub"]);
                syllabus.topic = Convert.ToString(dr["topic"]);
                syllabus.description = Convert.ToString(dr["description"]);
                syllabus.image = "~/UploadFile/" + Convert.ToString(dr["image"]);
                syllabus.part1 = "~/UploadFile/" + Convert.ToString(dr["part1"]);
                syllabus.part2 = "~/UploadFile/" + Convert.ToString(dr["part2"]);
               


                    
            }


            cn.Close();
            return View(syllabus);
        }

        public ActionResult ViewCategory()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<Class> category = new List<Class>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewcat", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                category.Add(
                     new Class
                     {

                         std = Convert.ToString(dr["std"]),

                     }
                    );
            }


            cn.Close();
            return View(category);

        }

        public ActionResult EditProfile(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblRegister where id = " + id, cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            Studendt_Register std = new Studendt_Register();

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {


                std.id = Convert.ToInt32(dr["id"]);
                std.firstname = Convert.ToString(dr["firstname"]);
                std.lastname = Convert.ToString(dr["lastname"]);
                std.phone = Convert.ToString(dr["phone"]);
                std.address = Convert.ToString(dr["address"]);
                std.state = Convert.ToString(dr["state"]);

            }
            return View(std);


        }

        [HttpPost]
        public ActionResult UpdateProfileData(Studendt_Register pd)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("UpdateProfile", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pd.id);
            cmd.Parameters.AddWithValue("@firstname", pd.firstname);
            cmd.Parameters.AddWithValue("@lastname", pd.lastname);
            cmd.Parameters.AddWithValue("@phone", pd.phone);
            cmd.Parameters.AddWithValue("@address", pd.address);
            cmd.Parameters.AddWithValue("@state", pd.state);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Profile");

        }

    }
}