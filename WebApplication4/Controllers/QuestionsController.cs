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
    public class QuestionsController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            if(id != 0)
            {
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");

                SqlCommand cmd2 = new SqlCommand("DELETE FROM questions WHERE id=@id", con2);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@id", id);

                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }


            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM  questions WHERE lesson_id=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();
            var questions = new List<Question>();

            while(dataReader.Read())
            {
                Question temp = new Question();
                temp.Id = (int)dataReader.GetValue(0);
                temp.Course_id = (int)dataReader.GetValue(1);
                temp.lesson_id = (int)dataReader.GetValue(2);
                temp.Name = dataReader.GetValue(3).ToString().Trim();
                temp.a= dataReader.GetValue(4).ToString().Trim();
                temp.b= dataReader.GetValue(5).ToString().Trim();
                temp.c= dataReader.GetValue(6).ToString().Trim();
                temp.d= dataReader.GetValue(7).ToString().Trim();
                temp.answer= dataReader.GetValue(8).ToString().Trim();
                temp.question_mp3 = dataReader.GetValue(9).ToString().Trim();
                temp.grade= dataReader.GetValue(10).ToString().Trim();
                temp.deficulty= dataReader.GetValue(11).ToString().Trim();

                questions.Add(temp);
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            if (questions.Count() == 0)
            {
                Question temp = new Question();
                temp.lesson_id = id;

                questions.Add(temp);
            }


            return View(questions);
        }


    }
}