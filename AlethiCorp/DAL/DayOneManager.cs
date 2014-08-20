using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
    public class DayOneManager : DayManager
    {
        public DayOneManager(DatabaseContext db, string userName)
          : base (db, userName)
        { }

        private void AddInterMails()
        {
            var interMails = new List<InterMail>
            {
              MakeMail("AlexDuMaurierWelcome"),
              MakeMail("OskarWebsite"),
              MakeMail("SandraWelcome"),
              MakeMail("SandraWeekly"),
              MakeMail("Benedetto")
            };
            interMails.ForEach(s => db.InterMails.Add(s));
        }

        private void AddSocialEvents()
        {
            var socialEvents = new List<SocialEvent>
            {
              MakeEvent("Team potluck!")
            };
            socialEvents.ForEach(e => db.SocialEvents.Add(e));
        }


        private void AddCourses()
        {
            var courses = new List<Course>
            {
              MakeCourse("Introduction to Haka")
            };
            courses.ForEach(c => db.Courses.Add(c));
        }

        private void AddNewsItems()
        {
            var newsItems = new List<NewsItem>
            {
              MakeNewsItem("DuMaurier"),
              MakeNewsItem("Contract"),
              MakeNewsItem("Welcome")
            };
            newsItems.ForEach(x => db.NewsItems.Add(x));
        }

        private void AddReports()
        {
            var Reports = new List<Report>
            {
              MakeReport("DayOneOnTheTracks"),
              MakeReport("DayOneMailBrightfieldCarbon"),
              MakeReport("DayOnePhoneBrightfieldCarbon"),
              MakeReport("DayOneMailBrightfieldCompass"),
              MakeReport("DayOnePhoneBrightfieldCompass"),
              MakeReport("DayOneMailBrightfieldGasparyan"),
              MakeReport("DayOneSurveillanceBrightfield"),
              MakeReport("DayOnePhoneCompassAbendroth"),
            };
            Reports.ForEach(x => db.Reports.Add(x));
        }

        public override void ActivateDay()
        {
            AddInterMails();

            AddSocialEvents();

            AddCourses();

            AddNewsItems();

            AddReports();

            db.SaveChanges();
        }
    }
}