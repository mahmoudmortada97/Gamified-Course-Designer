
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int Course_id { get; set; }
        public int lesson_id { get; set; }
        public string Name { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string answer{ get; set; }
        public string question_mp3 { get; set; }
        public string grade { get; set; }
        public string deficulty { get; set; }
 
    }
}