using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
    public enum ValueType
    {
        Aletheia,
        Integrity,
        Magnanimity
    }

    public enum AdventurousType
    {
        IsAdventurous,
        NotAdventurous
    }

    public enum NewSolutionsType
    {
        PreferNew,
        PreferOld
    }

    public class PersonalityTestViewModel
    {
       // DateTime? _date = NULL; //DateTime.Now.AddYears(40);

        [Required(ErrorMessage = "Please enter a valid color.")]
        [Display(Name = "What is your favorite color?")]
        public string FavoriteColor { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "What is your favorite date?")]
        [Required(ErrorMessage = "You must have a favorite date. How about your birthday, or something?")]
        public DateTime? FavoriteDate { get; set; }
        //{
        //    get { return _date; }
        //    set { _date = value; }
        //}

        [Display(Name = "If you were a bear, what type of bear would you be?")]
        [Required(ErrorMessage = "You must enter a valid type of bear.")]
        public string BearType { get; set; }

        [Display(Name = "On a scale from 0 to 9, how much of a team player are you?")]
        [Required(ErrorMessage = "All employees must be team players.")]
        [Range(0, 9, ErrorMessage = "Team player must be between 0 and 9.")]
        public int TeamPlayer { get; set; }

        [Display(Name = "Which of the three AlethiCorp core values best describe you: Aletheia, Integrity or Magnanimity?")]
        [Required(ErrorMessage = "You must have values.")]
        public ValueType ValueTypeSelection { get; set; }

        //[Display(Name = "Human rights...")]
        //[Required(ErrorMessage = "You must have an opinion on human rights.")]
        //public int HumanRights { get; set; }

        [Display(Name = "Are necessary to prevent human rights violations")]
        public bool RightsNecessary { get; set; }

        [Display(Name = "Are not necessary to prevent human rights violations")]
        public bool RightsNotNecessary { get; set; }

        [Display(Name = "Lack ontological parsimony")]
        public bool RightsParsimony { get; set; }

        [Display(Name = "A grizzly bear")]
        public bool RightsGrizzlyBear { get; set; }

        [Display(Name = "What is the greatest number of civilian casualties you would be willing to accept to eliminate a dangerous terrorist?")]
        [Required(ErrorMessage = "Surely, you have thought about this?")]
        [Range(4, int.MaxValue, ErrorMessage = "Please enter a number greater than 3.")]
        public int MaxCivilians { get; set; }

        [Display(Name = "Why do you want to work at AlethiCorp?")]
        [Required(ErrorMessage = "You haven't even thought about it, have you? Just another drone looking for a steady paycheck. I'll be in touch...")]
        public string WhyWork { get; set; }

        [Display(Name = "Do you prefer to follow the rules, or think for yourself?")]
        [Required(ErrorMessage = "Surely, you have thought about this?")]
        public bool FollowRules { get; set; }

        [Display(Name = "Would you consider yourself an adventurous person?")]
        [Required(ErrorMessage = "Surely, you have thought about this?")]
        public AdventurousType Adventurous { get; set; }

        [Display(Name = "Do you like coming up with new solutions, or do you stick with tried-and-true methods?")]
        [Required(ErrorMessage = "Surely, you have thought about this?")]
        public NewSolutionsType NewSolutions { get; set; }
    }
}