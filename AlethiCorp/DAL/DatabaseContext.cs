using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AlethiCorp.Models;

namespace AlethiCorp.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PersonalityTest> PersonalityTests { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<InterMail> InterMails { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SocialEvent> SocialEvents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<SentMail> SentMails { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
    }
}