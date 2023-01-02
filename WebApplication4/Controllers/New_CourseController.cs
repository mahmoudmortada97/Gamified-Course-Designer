
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Data;
using System.Data.SqlClient;  

namespace WebApplication4.Controllers
{
    public class New_CourseController : Controller
    {
        //
        // GET: /New_Course/
        public ActionResult Index()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Index(string name, string description, string requerment, string keywords,string Material_oj)
        {
            Course temp = new Course();
            temp.Name = name;
            temp.Description = description;
            temp.Requerment = requerment;
            temp.Keywords = keywords;
            temp.Material_oj = Material_oj;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("INSERT INTO [courses] (instructor_id, name, description, keywords, requerment,Material_oj) VALUES (@instructor_id, @name, @description, @keywords, @requerment,@Material_oj)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@instructor_id", ((Instructor)Session["user"]).Id);
            cmd.Parameters.AddWithValue("@name", temp.Name);
            cmd.Parameters.AddWithValue("@description", temp.Description);  
            cmd.Parameters.AddWithValue("@keywords", temp.Keywords);
            cmd.Parameters.AddWithValue("@requerment", temp.Requerment);
            cmd.Parameters.AddWithValue("@Material_oj", temp.Material_oj);  

            con.Open();  
            int k = cmd.ExecuteNonQuery();
            con.Close();  

            if (k != 0)
                return RedirectToAction("Index", "courses");
            else
                return RedirectToAction("Index", "new_course");

        }


    }
}