using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication4.Models;  

namespace WebApplication4.Controllers
{
    public class View_LessonController : Controller
    {
        //
        // GET: /View_Lesson/
        public ActionResult Index(int id)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM  lessons  WHERE id=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();

            Lesson temp = new Lesson();
            dataReader.Read();
            temp.Id = (int)dataReader.GetValue(0);
            temp.Course_id = (int)dataReader.GetValue(1);
            temp.Name = dataReader.GetValue(2).ToString().Trim();
            temp.Description = dataReader.GetValue(3).ToString().Trim();
            temp.order = dataReader.GetValue(4).ToString().Trim();
            temp.Material_text = dataReader.GetValue(5).ToString().Trim();
            temp.Material_vedio = dataReader.GetValue(6).ToString().Trim();
            temp.Material_image = dataReader.GetValue(7).ToString().Trim();
            temp.Material_mp3 = dataReader.GetValue(8).ToString().Trim();
            temp.Material_pdf = dataReader.GetValue(9).ToString().Trim();

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            return View(temp);
        }
	}
}