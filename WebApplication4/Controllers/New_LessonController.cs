
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
    public class New_LessonController : Controller
    {
        //
        // GET: /New_Course/
        public ActionResult Index(int id=0)
        {
            return View(id);
        }
 
        [HttpPost]
        public ActionResult Index(string course_id, string order, string name, string description, string material_text, HttpPostedFileBase material_vedio, HttpPostedFileBase material_mp3, HttpPostedFileBase material_pdf, HttpPostedFileBase material_image) 
        {
                Lesson temp = new Lesson();
                temp.Course_id = Int32.Parse(course_id);
                temp.Name = name;
                temp.order = order;
                temp.Description = description;
                temp.Material_text = material_text;

                temp.Material_image = "";
                temp.Material_pdf = "";
                temp.Material_mp3 = "";
                temp.Material_vedio = "";

                
                if (material_vedio != null && material_vedio.ContentLength > 0)
                {
                    temp.Material_vedio = Path.Combine(Server.MapPath("~/uploaded_files"), Path.GetFileName(material_vedio.FileName));
                    material_vedio.SaveAs(temp.Material_vedio);
                    temp.Material_vedio = "../uploaded_files/" + Path.GetFileName(material_vedio.FileName);
                }

                if (material_mp3 != null && material_mp3.ContentLength > 0)
                {
                    temp.Material_mp3 = Path.Combine(Server.MapPath("~/uploaded_files"), Path.GetFileName(material_mp3.FileName));
                    material_mp3.SaveAs(temp.Material_mp3);
                    temp.Material_mp3 = "../uploaded_files/" + Path.GetFileName(material_mp3.FileName);
                }

                if (material_image != null && material_image.ContentLength > 0)
                {
                    temp.Material_image = Path.Combine(Server.MapPath("~/uploaded_files"), Path.GetFileName(material_image.FileName));
                    material_image.SaveAs(temp.Material_image);
                    temp.Material_image = "../uploaded_files/" + Path.GetFileName(material_image.FileName);
                }

                if (material_pdf != null && material_pdf.ContentLength > 0)
                {
                    temp.Material_pdf = Path.Combine(Server.MapPath("~/uploaded_files"), Path.GetFileName(material_pdf.FileName));
                    material_pdf.SaveAs(temp.Material_pdf);
                    temp.Material_pdf = "../uploaded_files/" + Path.GetFileName(material_pdf.FileName);
                }

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mostafadoma\Desktop\WebApplication4\WebApplication4\App_Data\Database.mdf;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand("INSERT INTO [lessons] (course_id, name, description, [order], material_text, material_vedio, material_image, material_mp3, material_pdf) VALUES (@course_id, @name, @description, @order, @material_text, @material_vedio, @material_image, @material_mp3, @material_pdf)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@course_id", temp.Course_id);
                cmd.Parameters.AddWithValue("@name", temp.Name);
                cmd.Parameters.AddWithValue("@order", temp.order);
                cmd.Parameters.AddWithValue("@description", temp.Description);
                cmd.Parameters.AddWithValue("@material_text", temp.Material_text);
                cmd.Parameters.AddWithValue("@material_vedio", temp.Material_vedio);
                cmd.Parameters.AddWithValue("@material_image", temp.Material_image);
                cmd.Parameters.AddWithValue("@material_mp3", temp.Material_mp3);
                cmd.Parameters.AddWithValue("@material_pdf", temp.Material_pdf);

                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();

                if (k != 0)
                    return RedirectToAction("Index", "lessons");
                else
                    return RedirectToAction("Index", "new_lesson");


        }


    }
}