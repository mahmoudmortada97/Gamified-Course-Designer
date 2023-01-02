
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;  

namespace WebApplication4.Controllers
{
    public class New_QuestionController : Controller
    {
        //
        // GET: /New_Course/
        public ActionResult Index(int id = 0)
        {
            return View(id);
        }
 
        [HttpPost]
        public ActionResult Index(string lesson_id, string name, string a, string b, string c, string d, string answer, string grade, string deficulty, HttpPostedFileBase voice)
        {
            Question temp = new Question();
            temp.lesson_id = Int32.Parse(lesson_id);
            temp.Course_id = 0;
            temp.Name = name;
            temp.a = a;
            temp.b = b;
            temp.c = c;
            temp.d = d;
            temp.answer = answer;
            temp.grade = grade;
            temp.deficulty = deficulty;

            temp.question_mp3 = "";


            if (voice != null && voice.ContentLength > 0)
            {
                temp.question_mp3 = Path.Combine(Server.MapPath("/uploaded_files"), Path.GetFileName(voice.FileName));
                voice.SaveAs(temp.question_mp3);
                temp.question_mp3 = "../uploaded_files/" + Path.GetFileName(voice.FileName);
            }

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("INSERT INTO [questions] (lesson_id, course_id, name, a,  b, c, d, answer, voice, grade, deficulty) VALUES (@lesson_id, @course_id, @name, @a, @b, @c, @d, @answer, @voice, @grade, @deficulty)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@lesson_id", temp.lesson_id);
            cmd.Parameters.AddWithValue("@course_id", temp.Course_id);
            cmd.Parameters.AddWithValue("@name", temp.Name);
            cmd.Parameters.AddWithValue("@a", temp.a);
            cmd.Parameters.AddWithValue("@b", temp.b);
            cmd.Parameters.AddWithValue("@c", temp.c);
            cmd.Parameters.AddWithValue("@d", temp.d);
            cmd.Parameters.AddWithValue("@answer", temp.answer);
            cmd.Parameters.AddWithValue("@voice", temp.question_mp3);
            cmd.Parameters.AddWithValue("@grade", temp.grade);
            cmd.Parameters.AddWithValue("@deficulty", temp.deficulty);

            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();

            if (k != 0)
                return RedirectToAction("Index", "lessons");
            else
                return RedirectToAction("Index", "new_question");
           

        }


    }
}