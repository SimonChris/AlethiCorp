﻿using AlethiCorp.Models;
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
    }

    private void EndGameWithArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;

      FinishGame();
    }

    private bool InformedOskar()
    {
      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      return sentMail.Any(s => s.Recipient.ToLower().Contains("oskar") && s.GetContents().ToLower().ContainsAny(searchTerms));
    }

    private void EndGameWithDeletionArrest()
    {
      EndGameWithArrest();

      CachePlaythroughInformation("DayFiveArrestedDeletedAll");

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));
      if(andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedDeletedAllMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedDeletedAll"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
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

      CachePlaythroughInformation("ArrestedNoResults");

      db.NewsItems.Add(MakeNewsItem("DayFiveArrestedNoResults"));

      db.InterMails.Add(MakeMail("DayFiveSandraArrestedNoResults"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedNoResultsMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarArrestedNoResults"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
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

    private void EndGameWithFramedEmployees(bool framedAndrea)
    {
      EndGameWithArrest();

      CachePlaythroughInformation("FramedEmployees");

      db.NewsItems.Add(MakeNewsItem("DayFiveFramedEmployees"));

      db.InterMails.Add(MakeMail("DayFiveSandraFramedEmployees"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrested"));

      if (framedAndrea)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaFramedEmployees"));
      }
      else if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarFramedEmployeesMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarFramedEmployees"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();

      if (potluckContribution.Contains("spatula"))
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuFramedEmployeesSpatula"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveSalvinuFramedEmployees"));
      }

      db.SaveChanges();
    }

    private void EndGameWithDroneStrikes()
    {
      EndGameWithArrest();

      CachePlaythroughInformation("DroneStrikes");

      if (InformedOskar())
      {
        db.NewsItems.Add(MakeNewsItem("DayFiveDroneStrikesInformedOskar"));
      }
      else
      {
        db.NewsItems.Add(MakeNewsItem("DayFiveDroneStrikes"));
      }

      db.InterMails.Add(MakeMail("DayFiveSandraDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveVedeninArrestedDroneStrikes"));
      db.InterMails.Add(MakeMail("DayFiveBenedettoDroneStrikes"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaArrested"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarDroneStrikesMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarDroneStrikes"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
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

      CachePlaythroughInformation("Bear");

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveBear"));
      db.Reports.Add(MakeReport("DayFiveSurveillancePlayer"));

      db.SaveChanges();
    }

    public void JoinAndrea()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Andrea;
      db.Entry(gameState).State = EntityState.Modified;

      CachePlaythroughInformation("JoinAndrea");

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveAndrea"));
      db.Reports.Add(MakeReport("DayFiveSurveillanceAndrea"));

      db.SaveChanges();
    }

    private void EndGameWithSuccess()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Success;
      db.Entry(gameState).State = EntityState.Modified;

      CachePlaythroughInformation("Success");

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveSuccess"));

      db.InterMails.Add(MakeMail("DayFiveSandraSuccess"));
      db.InterMails.Add(MakeMail("DayFiveVedeninSuccess"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaSuccess"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarSuccessMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarSuccess"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
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

    private void EndGameWithKinsingerArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Success;
      db.Entry(gameState).State = EntityState.Modified;

      CachePlaythroughInformation("Kinsinger");

      FinishGame();

      db.NewsItems.Add(MakeNewsItem("DayFiveKinsinger"));

      db.InterMails.Add(MakeMail("DayFiveSandraKinsinger"));
      db.InterMails.Add(MakeMail("DayFiveVedeninKinsinger"));
      db.InterMails.Add(MakeMail("DayFiveVelikaKinsinger"));
      if (andreaImpressed)
      {
        db.InterMails.Add(MakeMail("DayFiveAndreaSuccess"));
      }

      if (InformedOskar())
      {
        db.InterMails.Add(MakeMail("DayFiveOskarKinsingerMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("DayFiveOskarKinsinger"));
      }
      var dayOne = db.GetDateString(0);
      var potluckEvent = db.SocialEvents.Where(x => x.UserName == UserName && x.Date == dayOne).Single();
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

    private string[] GetEmployeeNames()
    {
      return new string[] { "benedetto", "sandra", "oskar", "dumaurier", "andrea", "vitaly",
        "sháo", "jingfei", "salvinu" };
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName).ToList();
      var flaggedReports = extantReports.Where(x => x.Flagged).ToList();

      andreaImpressed = db.AndreaImpressed(UserName);

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
      var employeesFramed = threatsIdentified.Where(r => r.Name.ToLower().ContainsAny(GetEmployeeNames()));
      if (employeesFramed.Count() > 0)
      {
        EndGameWithFramedEmployees(employeesFramed.Where(r => r.Name.ToLower().Contains("andrea")).Any());
        return;
      }

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