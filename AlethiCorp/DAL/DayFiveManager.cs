using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
  public class DayFiveManager : DayManager
  {
    public DayFiveManager(DatabaseContext db, string userName)
      : base(db, userName)
    { }

    private void EndGameWithDeletionArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;

      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedDeletedAllMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedDeletedAll"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuArrestedSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuArrested"));
      }

      db.SaveChanges();
    }

    private void EndGameWithNoResultsArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;
      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedNoResults"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedNoResults"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedNoResultsMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedNoResults"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuNoResultsSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuNoResults"));
      }

      db.SaveChanges();
    }

    private void EndGameWithDroneStrikes()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;
      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("DayFiveDroneStrikes"));

      db.InterMails.Add(MakeMail("DayFiveSandraDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrestedDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveBenedettoDroneStrikes"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFiveOskarDroneStrikesMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarDroneStrikes"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuDroneStrikesSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuDroneStrikes"));
      }

      db.SaveChanges();
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName).ToList();
      var flaggedReports = extantReports.Where(x => x.Flagged).ToList();

      if (extantReports.Count() == 0)
      {
        EndGameWithDeletionArrest();
        return;
      }

      var recommendations = db.Recommendations.Where(r => r.UserName == UserName).ToList();
      var threatsIdentified = recommendations.Where(r => r.ThreatLevel > 5);

      if(threatsIdentified.Count() == 0)
      {
        EndGameWithNoResultsArrest();
        return;
      }

      var droneStrikes = recommendations.Where(r => r.DroneStrike);
      if(droneStrikes.Count() > 0)
      {
        EndGameWithDroneStrikes();
        return;
      }

      db.SaveChanges();
    }
  }
}