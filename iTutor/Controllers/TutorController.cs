using iTutor.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.IO;

namespace iTutor.Controllers
{
    public class TutorController : Controller
    {

        string connectionstring = @"Data Source=SURBHI;Initial Catalog=iTutor;Integrated Security=True";

        // GET: Tutor
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            return View();
        }

        public ActionResult Student_Entry()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<Studendt_Register> viewcustomerdetails = new List<Studendt_Register>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewstudent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                viewcustomerdetails.Add(
                     new Studendt_Register
                     {
                         id = Convert.ToInt32(dr["id"]),
                         firstname = Convert.ToString(dr["firstname"]),
                         lastname = Convert.ToString(dr["lastname"]),
                         address = Convert.ToString(dr["address"]),
                         state = Convert.ToString(dr["state"]),
                         phone = Convert.ToString(dr["phone"]),
                         gender = Convert.ToString(dr["gender"]),
                         email = Convert.ToString(dr["email"]),
                         password = Convert.ToString(dr["password"])

                     }
                    );
            }

            cn.Close();
            return View(viewcustomerdetails);

        }

        public ActionResult Addsyllabus()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<Class> listclasses = Classlist();
            ViewBag.listclasses = listclasses;
            return View();

        }

        [HttpPost]
        public ActionResult Addsyllabus(Syllabus asb, HttpPostedFileBase image, HttpPostedFileBase part1, HttpPostedFileBase part2)
        {

            if (!ModelState.IsValid)
            {
                List<Class> listclasses = Classlist();
                ViewBag.listclasses = listclasses;
                return View();
            }
            else
            {
                string _Filename = Path.GetFileName(image.FileName);
                string _Path = Path.Combine(Server.MapPath("~/UploadFile"), _Filename);
                image.SaveAs(_Path);

                string _Filename1 = Path.GetFileName(part1.FileName);
                string _Path1 = Path.Combine(Server.MapPath("~/UploadFile"), _Filename1);
                part1.SaveAs(_Path1);

                string _Filename2 = Path.GetFileName(part2.FileName);
                string _Path2 = Path.Combine(Server.MapPath("~/UploadFile"), _Filename2);
                part2.SaveAs(_Path2);

                List<SubList> lstsubject = new List<SubList>();
                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("Insert into TblSyllabus values (@std,@sub,@topic,@description,@image,@part1,@part2)", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@std", asb.std);
                cmd.Parameters.AddWithValue("@sub", asb.sub);
                cmd.Parameters.AddWithValue("@topic", asb.topic);
                cmd.Parameters.AddWithValue("@description", asb.description);
                cmd.Parameters.AddWithValue("@image",_Filename);
                cmd.Parameters.AddWithValue("@part1",_Filename1);
                cmd.Parameters.AddWithValue("@part2",_Filename2);
               
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return View("viewsyllabus");
            }

        }


        public List<Class> Classlist()
        {

            List<Class> listclass = new List<Class>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblClass", cn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {


                listclass.Add(
                   new Class
                   {

                       id = Convert.ToInt32(dr["id"]),
                       std = Convert.ToString(dr["std"])

                   }
                   );

            }
            cn.Close();
            return listclass;
        }

        public JsonResult Stdlist(int id)
        {
            List<SubList> lstsubject = new List<SubList>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblSubject where std ="+ id, cn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                lstsubject.Add(
                    new SubList
                    {
                        id = Convert.ToInt32(dr["id"]),
                        sub = Convert.ToString(dr["Subject"]),

                    }
                    );
            }
            cn.Close();
            return Json(new SelectList(lstsubject, "id", "sub")); ;
        }

        public ActionResult Topic()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            List<Syllabus> topic = Topiclist();
            ViewBag.syllabus = topic;
            return View();
        }


        [HttpPost]
        private List<Syllabus> Topiclist()
        {
            List<Syllabus> topic = new List<Syllabus>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblSyllabus", cn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                topic.Add(
                    new Syllabus
                    {

                        id = Convert.ToInt32(dr["id"]),
                        topic = Convert.ToString(dr["topic"]),


                    }
                    );
            }
            cn.Close();
            return topic;
        }

        [HttpPost]
        public ActionResult Topic(Assignment asb, HttpPostedFileBase assignment, HttpPostedFileBase testpaper)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                List<Class> listclasses = Classlist();
                ViewBag.listclasses = listclasses;
                return View();
            }
            else
            {
         

                string _Filename1 = Path.GetFileName(assignment.FileName);
                string _Path1 = Path.Combine(Server.MapPath("~/UploadFile"), _Filename1);
                assignment.SaveAs(_Path1);

                string _Filename2 = Path.GetFileName(testpaper.FileName);
                string _Path2 = Path.Combine(Server.MapPath("~/UploadFile"), _Filename2);
                testpaper.SaveAs(_Path2);

                List<SubList> lstsubject = new List<SubList>();
                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("addassignment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@topic", asb.topic);           
                cmd.Parameters.AddWithValue("@assignment", _Filename1);
                cmd.Parameters.AddWithValue("@testpaper", _Filename2);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return View("Student_Entry");
            }


        }

        public ActionResult viewsyllabus()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            List<Syllabus> viewcustomerdetails = new List<Syllabus>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblSyllabus", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
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
                         part2 = Convert.ToString(dr["part2"])
                         

                     }
                    );
            }

            cn.Close();
            return View(viewcustomerdetails);
        }

        public ActionResult TutorProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            List<tutor_register> viewcustomerdetails = new List<tutor_register>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from Tutor_Register where id=" + Session["UserId"], cn);
            //cmd.CommandType = CommandType.StoredProcedure;
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
                         address = Convert.ToString(dr["address"]),
                         state = Convert.ToString(dr["state"]),
                         city = Convert.ToString(dr["city"]),
                         qualification = Convert.ToString(dr["qualification"]),
                         experience = Convert.ToString(dr["experience"]),
                         dob = Convert.ToString(dr["dob"]),
                         phone = Convert.ToString(dr["phone"]),
                         gender = Convert.ToString(dr["gender"]),
                         email = Convert.ToString(dr["email"]),
                         password = Convert.ToString(dr["password"])

                     }
                    );
            }


            cn.Close();
            return View(viewcustomerdetails);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["email"] = null;
            return RedirectToAction("Student_Login", "Home");
        }


    }
}