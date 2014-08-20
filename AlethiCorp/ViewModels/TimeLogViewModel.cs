using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
  public class TimeLogViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Number of hours worked")]
    [Required(ErrorMessage = "You must enter the number of hours you have worked today.")]
    [Range(8, 8, ErrorMessage = "You must work 8 hours per day.")]
    public int HoursWorked { get; set; }
  }
}