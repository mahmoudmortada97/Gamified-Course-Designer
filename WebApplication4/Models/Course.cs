using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Instructor_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requerment { get; set; }
        public string Keywords { get; set; }
        public int NumberOfLessons { get; set; }
        public string Material_oj { get; set; }

    }
}