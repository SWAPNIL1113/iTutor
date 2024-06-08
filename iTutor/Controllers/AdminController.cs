using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTutor.Models;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using System.Web.WebPages;
using System.IO;

namespace iTutor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        string connectionstring = @"Data Source=SURBHI;Initial Catalog=iTutor;Integrated Security=True";

        public ActionResult Home()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("countregisteruser", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cn.Open();
            Object Count = cmd.ExecuteScalar();

            cmd.ExecuteNonQuery();

            cn.Close();
            ViewBag.count = Count;

            return View();
        }

        public ActionResult Tutor_Entry()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            List<tutor_register> viewcustomerdetails = new List<tutor_register>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewtutor", cn);
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
        public ActionResult deletedata(int id)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("DeleteRegister", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Student_Entry", "Admin");
        }

        public ActionResult deletetutor(int id)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("DeleteTutor", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Tutor_Entry", "Admin");
        }

        public ActionResult Class_Entry()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            return View();

        }

        [HttpPost]
        public ActionResult Class_Entry(Class ss)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {

                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("Insert into TblClass values (@std)", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@std", ss.std);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return View("Class_Entry");
            }
        }

        public ActionResult viewclassentry()
        {
        
            List<Class> viewcustomerdetails = new List<Class>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewclassentry", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cse = new SqlDataAdapter(cmd);   
            DataTable dt = new DataTable();
            cn.Open();
            cse.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                viewcustomerdetails.Add(
                     new Class
                     {
                         id = Convert.ToInt32(dr["id"]),
                         std = Convert.ToString(dr["std"]),

                     }
                    );
            }

            cn.Close();
            return View(viewcustomerdetails);
        }

        public ActionResult Sub_Entry()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            List<Class> lststudents = Stdlist();
            ViewBag.city = lststudents;
            return View();

        }

        [HttpPost]
        public ActionResult Sub_Entry(SubList SE)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {

                SqlConnection cn = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("Insert into TblSubject values (@subject,@std)", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@subject", SE.sub);
                cmd.Parameters.AddWithValue("@std", SE.std);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction("viewsubentry");
            }
        }

        public ActionResult DeleteStd(int id)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("DeleteStd", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("viewclassentry", "Admin");
        }

        public ActionResult viewsubentry()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }

            List<SubList> viewcustomerdetails = new List<SubList>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("viewsub", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cse = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            cse.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                viewcustomerdetails.Add(
                     new SubList
                     {
                         id = Convert.ToInt32(dr["id"]),
                         sub = Convert.ToString(dr["subject"]),
                         std = Convert.ToString(dr["std"]),

                     }
                    );
            }

            cn.Close();
            return View(viewcustomerdetails);
        }

        [HttpPost]
        private List<Class> Stdlist()
        {
           
            List<Class> lststudents = new List<Class>();
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblClass", cn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                lststudents.Add(
                    new Class
                    {

                        id = Convert.ToInt32(dr["id"]),
                        std = Convert.ToString(dr["std"]),


                    }
                    );
            }
            cn.Close();
            return lststudents;
        }

        public ActionResult Uploaded_StudyMaterial()
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

        public ActionResult deletesub(int id)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("DeleteSub", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("viewsubentry", "Admin");
        }

        public ActionResult deletesyllabus(int id)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("DeleteSyllabus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Uploaded_StudyMaterial", "Admin");
        }


        //private List<Class> Companylist()
        //{

        //    List<Class> lststudents = new List<Class>();
        //    SqlConnection cn = new SqlConnection(connectionstring);
        //    SqlCommand cmd = new SqlCommand("select * from TblClass", cn);
        //    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    cn.Open();
        //    adp.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        lststudents.Add(
        //            new Class
        //            {

        //                id = Convert.ToInt32(dr["id"]),
        //               std = Convert.ToString(dr["class1"]),

        //            }
        //            );
        //    }
        //    cn.Close();
        //    return lststudents;
        //}

        //public JsonResult getmodel(int id)
        //{

        //    List<SubList> lststudents = new List<SubList>();
        //    SqlConnection cn = new SqlConnection(connectionstring);
        //    SqlCommand cmd = new SqlCommand("select * from TblSub where TblClass=" + id, cn);
        //    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    cn.Open();
        //    adp.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        lststudents.Add(
        //            new SubList
        //            {

        //                id = Convert.ToInt32(dr["id"]),
        //                sub = Convert.ToString(dr["sub"]),


        //            }
        //            );
        //    }
        //    cn.Close();
        //    return Json(lststudents, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult EditStd(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblClass where id = " + id, cn); 
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            Class std = new Class();

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {


                std.id = Convert.ToInt32(dr["id"]);
                std.std = Convert.ToString(dr["std"]);
                
            }
            return View(std);


        }

        [HttpPost]
        public ActionResult UpdateStdData(Class pd)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("UpdateStd", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pd.id);
            cmd.Parameters.AddWithValue("@std", pd.std);
            
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("viewclassentry");

        }

        public ActionResult EditSub(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Student_Login", "Home");
            }
            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select * from TblSubject where id = " + id, cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SubList std = new SubList();

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cn.Open();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {


                std.id = Convert.ToInt32(dr["id"]);
                std.sub = Convert.ToString(dr["subject"]);

            }
            return View(std);


        }

        [HttpPost]
        public ActionResult UpdateSubData(SubList pd)
        {

            SqlConnection cn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("UpdateSub", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pd.id);
            cmd.Parameters.AddWithValue("@subject", pd.sub);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("viewsubentry");

        }



    }
}

