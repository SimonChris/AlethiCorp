using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
  public class Recommendation
  {
    public int Id { get; set; }

    [ScaffoldColumn(false)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "A recommendation must tied to a specific person")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Threat Level must be a number")]
    [Display(Name = "Threat Level")]
    public int ThreatLevel { get; set; }

    [Required(ErrorMessage = "Subject must be assigned to a Threat Type")]
    [Display(Name = "Threat Type")]
    public string ThreatType { get; set; }

    [Display(Name = "Drone Strike")]
    public bool DroneStrike { get; set; }

    [Required(ErrorMessage = "Please make an observation of some kind")]
    [DataType(DataType.MultilineText)]
    public string Comments { get; set; }
  }
}