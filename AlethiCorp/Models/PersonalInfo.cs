using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    public class PersonalInfo
    {
        [Key]
        public string UserName { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Male { get; set; }

        public int Age { get; set; }

        public bool LeftWing { get; set; }
    }
}