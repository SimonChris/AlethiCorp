using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    public class SocialEvent
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public bool Attending { get; set; }

        public string Contribution { get; set; }

        public bool Enabled { get; set; }
    }
}