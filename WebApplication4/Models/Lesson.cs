using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int Course_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string order { get; set; }
        public string Material_text { get; set; }
        public string Material_vedio { get; set; }
        public string Material_image { get; set; }
        public string Material_mp3 { get; set; }
        public string Material_pdf { get; set; }
        public int NumberOfQuestions { get; set; }


    }
}