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
    public class Edit_courseController : Controller
    {
        //
        // GET: /Edit_user/
        public ActionResult Index(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM  courses  WHERE id=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();
            Course course = new Course();

            while (dataReader.Read())
            {
                course.Id = (int)dataReader.GetValue(0);
                course.Instructor_id = (int)dataReader.GetValue(1);
                course.Name = dataReader.GetValue(2).ToString().Trim();
                course.Description = dataReader.GetValue(3).ToString().Trim();
                course.Requerment = dataReader.GetValue(4).ToString().Trim();
                course.Keywords = dataReader.GetValue(5).ToString().Trim();
                course.Material_oj = dataReader.GetValue(6).ToString().Trim();
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            return View(course);
        }

        [HttpPost]
        public ActionResult Index(string name, string description, string requerment, string keywords, string id,string Material_oj)
        {
            Course temp = new Course();
            temp.Id = Int32.Parse(id);
            temp.Name = name;
            temp.Description = description;
            temp.Requerment = requerment;
            temp.Keywords = keywords;
            temp.Material_oj = Material_oj;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("UPDATE [courses] SET name=@name, description=@description, requerment=@requerment, keywords=@keywords,Material_oj=@Material_oj WHERE id=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", temp.Id);
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
                return RedirectToAction("Index", "edit_course");

        }


    }
}