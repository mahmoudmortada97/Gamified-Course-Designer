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
    public class LessonsController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            if(id != 0)
            {
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
                SqlCommand cmd2 = new SqlCommand("DELETE FROM lessons WHERE id=@id; DELETE FROM questions WHERE lesson_id=@id;", con2);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@id", id);

                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }

            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM  lessons LEFT JOIN (SELECT lesson_id, count(lesson_id)   as c FROM questions GROUP BY lesson_id) as T2 ON lessons.id = T2.lesson_id where course_id=@id  ORDER BY 'order' ASC", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();
            var lessons = new List<Lesson>();

            while(dataReader.Read())
            {
                Lesson temp = new Lesson();
                temp.Id = (int)dataReader.GetValue(0);
                temp.Course_id= (int)dataReader.GetValue(1);
                temp.Name = dataReader.GetValue(2).ToString().Trim();
                temp.Description = dataReader.GetValue(3).ToString().Trim();
                temp.order = dataReader.GetValue(4).ToString().Trim();
                temp.Material_text= dataReader.GetValue(5).ToString().Trim();
                temp.Material_vedio = dataReader.GetValue(6).ToString().Trim();
                temp.Material_image = dataReader.GetValue(7).ToString().Trim();
                temp.Material_mp3 = dataReader.GetValue(8).ToString().Trim();
                temp.Material_pdf = dataReader.GetValue(9).ToString().Trim();
                temp.NumberOfQuestions = (dataReader.GetValue(11).ToString() != "") ? (int)dataReader.GetValue(11) : 0;

                lessons.Add(temp);
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            if(lessons.Count() == 0)
            {
                Lesson temp = new Lesson();
                temp.Course_id = id;

                lessons.Add(temp);
            }

            return View(lessons);
        }


    }
}