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
    public class CoursesController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            if(id != 0)
            {
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");

                SqlCommand cmd2 = new SqlCommand("DELETE FROM courses WHERE id=@id; DELETE FROM lessons WHERE course_id=@id;", con2);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@id", id);

                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
            int c;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM  courses LEFT JOIN (SELECT course_id, count(course_id) as c FROM lessons GROUP BY course_id) as T2 ON courses.id = T2.course_id WHERE instructor_id=@id", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", ((Instructor)Session["user"]).Id);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();
            var courses = new List<Course>();
        
            while(dataReader.Read())
            {
                Course temp = new Course();
                temp.Id = (int)dataReader.GetValue(0);
                temp.Instructor_id = (int)dataReader.GetValue(1);
                temp.Name = dataReader.GetValue(2).ToString().Trim();
                temp.Description = dataReader.GetValue(3).ToString().Trim();
                temp.Requerment = dataReader.GetValue(4).ToString().Trim();
                temp.Keywords = dataReader.GetValue(5).ToString().Trim();
                temp.NumberOfLessons = (dataReader.GetValue(7).ToString() != "") ? (int)dataReader.GetValue(7) : 0 ;               
                courses.Add(temp);
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            return View(courses);
        }


    }
}