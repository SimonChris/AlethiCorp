using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
    public enum GenderType
    {
        Male,
        Female
    }

    public enum PoliticsType
    {
        Left,
        Right
    }

    public class PersonalInfoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AlethiCorp does not employ people with no name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "If you do not remember your last name, please ask a family member.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must have a gender.")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "You have to face it.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Everyone has politics.")]
        [Display(Name = "Political Orientation")]
        public PoliticsType Politics { get; set; }

    }
}