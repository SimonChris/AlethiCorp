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
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("alex"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var contactAttempts = db.SentMails.Where(r => r.UserName == UserName).ToList().Where(r => r.Recipient.ToLower().Contains("alex"));
        if (contactAttempts.Count() > 2)
        {
          db.InterMails.Add(MakeMail("DayFourAlexPersistence", subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayFourAlexReply", subject));
        }
      }
    }

    private void AddAndreaReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("andrea"));
      if (searchResult.Count() > 0)
      {
        var nellieResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(new string[] { "nellie", " bly", "nelly" }));
        var nelieResults = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(new string[] { "nelie", " nely" }));
        if (nelieResults.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayFourAndreaNelie", "Re: " + nelieResults.First().Subject));
        }
        else if (nellieResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayFourAndreaNellie", "Re: " + nellieResult.First().Subject));
        }
        else
        {
          db.Reports.Add(MakeReport("DayFourInterMailReview"));
        }
      }
    }

    private void AddBenedettoReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("benedetto"));
      if (searchResult.Count() > 0)
      {
        var hackingProgression = db.GetHackingProgression(UserName);
        var subject = "Re: " + searchResult.First().Subject;
        if (hackingProgression == HackingProgression.Concurrency)
        {
          db.InterMails.Add(MakeMail("DayFourBenedettoConcurrency", subject));
        }
        else if (hackingProgression == HackingProgression.Infiltrator)
        {
          db.InterMails.Add(MakeMail("DayFourBenedettoInfiltrator", subject));

        }
        else
        {
          db.InterMails.Add(MakeMail("DayFourBenedettoReply", subject));
        }
      }
    }

    private void AddShaoReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().ContainsAny(new string[] { "sháo", "Jingfei" }));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "gilbert", "chesterton" };
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayFourShaoChesterton", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayFourShaoReply", subject));
        }
      }
    }

    private void AddOmegaReplies(List<SentMail> sentMails)
    {

    }

    private void AddOskarReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("oskar"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;

        var searchTerms = new string[] { "omega", "forwarded message" };
        var oldSentMail = db.SentMails.Where(s => s.UserName == UserName).ToList().Where(s => Convert.ToInt32(s.Date) < 2);
        bool informedOskar = oldSentMail.Any(s => s.GetContents().ToLower().ContainsAll(searchTerms));
        if (informedOskar)
        {
          db.InterMails.Add(MakeMail("DayFourOskarOmegaOngoing", subject));
        }
        else
        {
          var detailedResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAll(searchTerms));
          if (detailedResult.Count() > 0)
          {
            db.InterMails.Add(MakeMail("DayFourOskarOmega", "Re: " + detailedResult.First().Subject));
          }
          else
          {
            db.InterMails.Add(MakeMail("DayFourOskarReply", subject));
          }
        }
      }
    }

    private void AddSandraReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("sandra"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var trainResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(new string[] { "train" }));
        if (trainResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayFourSandraTrain", "Re: " + trainResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayFourSandraReply", "Re: " + subject));
        }
      }
    }

    private void AddSalvinuReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().ContainsAny(new string[] { "salvinu" }));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "soupe", "recipe" };
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayFourSalvinuSoupe", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayFourSalvinuReply", subject));
        }
      }
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
      AddSalvinuReplies(sentMails);
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
      if (reviewed.Contains("DayThreeMailBlackUnknown"))
      {
        results++;
        newReports.Add(MakeReport("DayFourInterviewBlue"));
      }
      if(reviewed.Contains("DayThreeSurveillanceMarian"))
      {
        results++;
        newReports.Add(MakeReport("DayFourSurveillanceMarian"));
      }
      if (db.Reports.Any(r => r.UserName == UserName && r.Name == "DayOneMailBrightfieldGasparyan" && r.Flagged))
      {
        results++;
        newReports.Add(MakeReport("DayFourPhoneBrightfieldGasparyan"));
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

      if (results > 1)
      {
        newMails.Add(MakeMail("DayFourSalvinuManyResults"));
      }
      else if (results > 0)
      {
        newMails.Add(MakeMail("DayFourSalvinuResults"));
      }
      else
      {
        newMails.Add(MakeMail("DayFourSalvinuNoResults"));
      }

      int bearCount = 0;
      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();
      var bear = db.GetBearType(UserName).ToLower();
      var bearTerms = new string[] { "bear", bear };
      if (potluckContribution.ContainsAny(bearTerms))
      {
        bearCount++;
      }
      var onboardingEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(1)).Single();
      var onboardingContribution = onboardingEvent.Contribution.ToLower();
      if (onboardingContribution.ContainsAny(bearTerms))
      {
        bearCount++;
      }
      var clubNightEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(2)).Single();
      var clubNightContribution = clubNightEvent.Contribution.ToLower();
      var bearNight = clubNightContribution.ContainsAny(bearTerms);
      if (bearNight)
      {
        bearCount++;
      }
      if (!clubNightEvent.Attending)
      {
        newMails.Add(MakeMail("DayFourBenedettoNoShow"));
      }
      else if (clubNightContribution.Count() == 0 || clubNightContribution.Contains("please don't enter this text"))
      {
        newMails.Add(MakeMail("DayFourBenedettoNoContrib"));
      }
      else if (bearNight)
      {
        switch (bearCount)
        {
          case 1:
            newMails.Add(MakeMail("DayFourBenedettoGrizzly"));
            break;
          case 2:
            newMails.Add(MakeMail("DayFourBenedettoDoubleGrizzly"));
            break;
          case 3:
            newMails.Add(MakeMail("DayFourBenedettoTripleGrizzly"));
            break;
        }
      }
      else if (clubNightContribution.ToLower().Contains(db.GetFavoriteColor(UserName).ToLower() + " russian"))
      {
        newMails.Add(MakeMail("DayFourBenedettoColor"));
      }
      else
      {
        newMails.Add(MakeMail("DayFourBenedettoContrib"));
      }

      var hackingProgression = db.GetHackingProgression(UserName);
      if (hackingProgression == HackingProgression.Concurrency)
      {
        newMails.Add(MakeMail("DayFourOmegaConcurrency"));
      }
      else if (hackingProgression == HackingProgression.Infiltrator)
      {
        newMails.Add(MakeMail("DayFourOmegaInfiltrator"));
      }
      else
      {
        newMails.Add(MakeMail("DayFourAlphaNoInfiltrator"));
      }

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

      CachePlaythroughInformation("DayFourArrestedDeletedAll");

      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("DayFourArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("DayFourSandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("DayFourVedeninArrested"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.Recipient.ToLower().Contains("oskar") && s.GetContents().ToLower().ContainsAny(searchTerms));
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