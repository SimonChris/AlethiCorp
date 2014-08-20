using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    public class SentMail
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "If you send a mail, but no one receives it, does it exist?")]
        public string Recipient { get; set; }

        [Required(ErrorMessage = "A mail about nothing will accomplish nothing")]
        public string Subject { get; set; }

        [ScaffoldColumn(false)]
        public string Date { get; set; }

        [Required(ErrorMessage = "A mail with no content is like a broken pencil: pointless")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        public bool Read { get; set; }

    }
}