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
    public class Edit_userController : Controller
    {
        //
        // GET: /Edit_user/
        public ActionResult Index()
        {
            return View();
        }

            [HttpPost]
        public ActionResult Index(string name, string phone, string email, string password, string id)
        {
            Instructor user = new Instructor();
            user.Id = Int32.Parse(id);
            user.Name = name;
            user.Phone = phone;
            user.Email = email;
            user.Password = password;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("UPDATE [instructors] SET name=@name, phone=@phone, email=@email, password=@password WHERE id=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", user.Id);  
            cmd.Parameters.AddWithValue("@name", user.Name);  
            cmd.Parameters.AddWithValue("@phone", user.Phone);  
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
                return RedirectToAction("Index", "edit_user");
            }

        }


    }
}