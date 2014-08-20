using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Display(Name = "Course")]
        public string Title { get; set; }

        public bool Completed { get; set; }

        public string Grade { get; set; }

        [ScaffoldColumn(false)]
        public string Answer { get; set; }
    }
}