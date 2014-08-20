using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
    public class InterMailViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        public bool Read { get; set; }
    }
}