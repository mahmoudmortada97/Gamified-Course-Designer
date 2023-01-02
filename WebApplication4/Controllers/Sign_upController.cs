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
    public class Sign_upController : Controller
    {
        //
        // GET: /Sign_up/
        public ActionResult Index()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Index(string name, string phone, string gender, string email, string password)
        {
            Instructor user = new Instructor();
            user.Name = name;
            user.Phone = phone;
            user.Gender = gender;
            user.Email = email;
            user.Password = password;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("INSERT INTO [instructors] (name, phone, gender, email, password) VALUES (@name, @phone, @gender, @email, @password)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", user.Name);  
            cmd.Parameters.AddWithValue("@phone", user.Phone);  
            cmd.Parameters.AddWithValue("@gender", user.Gender);  
            cmd.Parameters.AddWithValue("@email", user.Email);  
            cmd.Parameters.AddWithValue("@password", user.Password);  

            con.Open();  
            int k = cmd.ExecuteNonQuery();
            con.Close();  

            if (k != 0)
            {
                Session["user"] = user;
                return RedirectToAction("Index", "courses");
            }
            else
            {
                return RedirectToAction("Index", "sign_up");
            }

        }


    }
}