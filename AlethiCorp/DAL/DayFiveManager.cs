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

    private bool andreaImpressed;

    private void FinishGame()
    {
      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      List<Recommendation> recommendations = db.Recommendations.Where(m => m.UserName == UserName).ToList();
      recommendations.ForEach(r => db.Recommendations.Remove(r));
    }

    private void EndGameWithArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;

      FinishGame();
    }

    private void EndGameWithDeletionArrest()
    {
      EndGameWithArrest();

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));
      if(andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

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
      EndGameWithArrest();

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedNoResults"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedNoResults"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

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
      EndGameWithArrest();

      db.NewsItems.Add(MakeNewsItem("DayFiveDroneStrikes"));

      db.InterMails.Add(MakeMail("DayFiveSandraDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrestedDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveBenedettoDroneStrikes"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

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

    public void ReleaseBear()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Bear;
      db.Entry(gameState).State = EntityState.Modified;

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveBear"));
      db.Reports.Add(MakeReport("DayFiveSurveillancePlayer"));

      db.SaveChanges();
    }

    public void EndGameWithSuccess()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Success;
      db.Entry(gameState).State = EntityState.Modified;

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveSuccess"));

      db.InterMails.Add(MakeMail("DayFiveSandraSuccess"));
      db.InterMails.Add(MakeMail("DayFiveVedeninSuccess"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFiveOskarSuccessMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarSuccess"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuSuccessSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuSuccess"));
      }

      db.SaveChanges();
    }

    public void EndGameWithKinsingerArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Success;
      db.Entry(gameState).State = EntityState.Modified;

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveKinsinger"));

      db.InterMails.Add(MakeMail("DayFiveSandraKinsinger"));
      db.InterMails.Add(MakeMail("DayFiveVedeninKinsinger"));
      db.InterMails.Add(MakeMail("DayFiveVelikaKinsinger"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("DayFiveOskarKinsingerMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarKinsinger"));
      }
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuKinsingerSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuKinsinger"));
      }

      db.SaveChanges();
    }

    private string[] GetCredibleThreatNames()
    {
      return new string[] { "martin", "patricia", "silva", "alyona", "jaspers", "john blue",
        "adroushan", "john compass", "samuel", "hannah", "absolon", "victor" };
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName).ToList();
      var flaggedReports = extantReports.Where(x => x.Flagged).ToList();

      var andreaMails = db.SentMails.Where(r => r.UserName == UserName && r.Recipient.ToLower().Contains("andrea")).ToList();
      bool andreaQuestion = andreaMails.Where(r => r.Date == "2" && r.GetContents().ToLower().ContainsAny(new string[] { "nellie", " bly", "nelly" })).Any();
      bool andreaAnswer = andreaMails.Where(r => r.Date == "3" && r.GetContents().ToLower().ContainsAll(new string[] { "gasp", " laugh" })).Any();
      andreaImpressed = andreaQuestion && andreaAnswer;

      if (extantReports.Count() == 0)
      {
        EndGameWithDeletionArrest();
        return;
      }

      var recommendations = db.Recommendations.Where(r => r.UserName == UserName).ToList();

      var droneStrikes = recommendations.Where(r => r.DroneStrike);
      if(droneStrikes.Count() > 0)
      {
        EndGameWithDroneStrikes();
        return;
      }

      var threatsIdentified = recommendations.Where(r => r.ThreatLevel > 5);
      if(threatsIdentified.Any(t => t.Name.ToLower().Contains("kinsinger")))
      {
        EndGameWithKinsingerArrest();
        return;
      }

      var credibleThreatCount = threatsIdentified.Where(r => r.Name.ToLower().ContainsAny( GetCredibleThreatNames() )).Count();
      if(credibleThreatCount > 0)
      {
        EndGameWithSuccess();
        return;
      }

      EndGameWithNoResultsArrest();
    }
  }
}