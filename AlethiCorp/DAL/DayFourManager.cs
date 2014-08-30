using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
  public class DayFourManager : DayManager
  {
    public DayFourManager(DatabaseContext db, string userName)
      : base(db, userName)
    { }

    private readonly int maxReviewed = 4;

    private void AddAlexReplies(List<SentMail> sentMails)
    {

    }

    private void AddAndreaReplies(List<SentMail> sentMails)
    {

    }

    private void AddBenedettoReplies(List<SentMail> sentMails)
    {

    }

    private void AddShaoReplies(List<SentMail> sentMails)
    {

    }

    private void AddOmegaReplies(List<SentMail> sentMails)
    {

    }

    private void AddOskarReplies(List<SentMail> sentMails)
    {

    }

    private void AddSandraReplies(List<SentMail> sentMails)
    {

    }

    private void AddVitalyReplies(List<SentMail> sentMails)
    {

    }

    private void AddReplies()
    {
      var sentMails = db.SentMails.Where(r => r.UserName == UserName && r.Date == "2").ToList();
      if (sentMails.Count() == 0)
      {
        return;
      }

      AddAlexReplies(sentMails);
      AddAndreaReplies(sentMails);
      AddBenedettoReplies(sentMails);
      AddShaoReplies(sentMails);
      AddOmegaReplies(sentMails);
      AddOskarReplies(sentMails);
      AddSandraReplies(sentMails);
      AddVitalyReplies(sentMails);
    }

    private void AddUnconditionalReports(List<Report> newReports)
    {
      newReports.Add(MakeReport("DayFourSurveillanceStudentMeeting"));
    }

    private int AddReports(List<Report> extantReports, List<Report> flaggedReports)
    {
      var newReports = new List<Report>();
      AddUnconditionalReports(newReports);

      var reviewed = new List<string>();
      foreach (var report in flaggedReports.SelectRandomSubset(maxReviewed))
      {
        reviewed.Add(report.Name);
      }

      int results = 0;
      if(reviewed.Contains("DayThreeMailBlackUnknown"))
      {
        results++;
        newReports.Add(MakeReport("DayFourInterviewBlue"));
      }
      newReports.ForEach(x => db.Reports.Add(x));
      return results;
    }

    private void AddInterMails(List<Report> extantReports, List<Report> flaggedReports, int results)
    {
      AddReplies();
      var extantCount = extantReports.Count();
      int flaggedCount = flaggedReports.Count;

      var newMails = new List<InterMail>();

      newMails.Add(MakeMail("DayFourSandraReminder"));

      newMails.ForEach(s => db.InterMails.Add(s));
    }

    private void AddNewsItems()
    {
      var newsItems = new List<NewsItem>
          {
              MakeNewsItem("InvestigationUpdate")
          };
      newsItems.ForEach(x => db.NewsItems.Add(x));
    }

    private void UpdateSocialEvents()
    {
      var clubNight = db.SocialEvents.Where(x => x.UserName == UserName && x.Title.ToLower().Contains("team club night")).Single();
      clubNight.Enabled = false;
      db.Entry(clubNight).State = EntityState.Modified;
    }

    private void EndGameWithArrest()
    {
      UpdateSocialEvents();

      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;

      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("DayFourArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("DayFourSandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("DayFourVedeninArrested"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFourOskarArrestedDeletedAllMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFourOskarArrestedDeletedAll"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFourSalvinuArrestedSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFourSalvinuArrested"));
      }

      db.SaveChanges();
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName && x.Day == 2).ToList();
      var flaggedReports = extantReports.Where(x => x.Flagged).ToList();

      if (extantReports.Count() == 0)
      {
        EndGameWithArrest();
        return;
      }

      int results = AddReports(extantReports, flaggedReports);

      AddInterMails(extantReports, flaggedReports, results);

      AddNewsItems();

      UpdateSocialEvents();

      db.SaveChanges();
    }
  }
}