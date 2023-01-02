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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            Instructor user = new Instructor();
            user.Email = email;
            user.Password = password;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM [instructors] WHERE email =@email AND password=@password", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);

            con.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();

            if(dataReader.Read())
            {
                user.Id = (int)dataReader.GetValue(0);
                user.Name = dataReader.GetValue(1).ToString().Trim();
                user.Phone = dataReader.GetValue(2).ToString().Trim();
                user.Gender = dataReader.GetValue(3).ToString().Trim();

                dataReader.Close();
                cmd.Dispose();
                con.Close();

                Session["user"] = user;
                return RedirectToAction("Index", "courses");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


    }
}