using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    [Flags]
    public enum HumanRightsValues
    {
        Necessary = 1,
        NotNecessary = 2,
        Parsimony = 4,
        GrizzlyBear = 8
    }

    public static class HumanRightsValuesExtensions
    {
        public static HumanRightsValues Set(this HumanRightsValues rights, HumanRightsValues flags)
        {
            return rights | flags;
        }
    }

    public class PersonalityTest
    {
        [Key]
        public string UserName { get; set; } 

        public string FavoriteColor { get; set; }

        public DateTime FavoriteDate { get; set; }

        public string BearType { get; set; }

        public int TeamPlayer { get; set; }

        public int ValueTypeSelection { get; set; }

        public HumanRightsValues HumanRights { get; set; }

        public int MaxCivilians { get; set; }

        public bool FollowRules { get; set; }

        public bool Adventurous { get; set; }

        public bool NewSolutions { get; set; }
    }
}