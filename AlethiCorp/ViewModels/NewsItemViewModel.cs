using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
    public class NewsItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Name { get; set; }

        [Required]
        public string HeadLine { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string MainText { get; set; }

        [Required]
        public string Date { get; set; }
    }
}