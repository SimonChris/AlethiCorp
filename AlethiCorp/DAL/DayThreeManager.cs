using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
  public class DayThreeManager : DayManager
  {
    public DayThreeManager(DatabaseContext db, string userName)
      : base(db, userName)
    { }

    private readonly int maxReviewed = 4;

    private void AddAlexReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("alex"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("DayThreeAlexReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddAndreaReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("andrea"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var goldmanResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(new string[] { "emma", " goldman", "mother earth" }));
        var golmanResults = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(new string[] { "golman" }));
        if (golmanResults.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayThreeAndreaGolman", "Re: " + golmanResults.First().Subject));
        }
        else if (goldmanResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayThreeAndreaGoldman", "Re: " + goldmanResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayThreeAndreaReply", "Re: " + subject));
        }
      }
    }

    private void AddBenedettoReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("benedetto"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "iam", "has sent me" };
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          var state = db.GameStates.Where(s => s.UserName == UserName).Single();
          state.HackingProgression = HackingProgression.Infiltrator;
          db.Entry(state).State = EntityState.Modified;
          db.InterMails.Add(MakeMail("BenedettoIAM", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("BenedettoReplyDayThree", subject));
        }
      }
    }

    private void AddShaoReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().ContainsAny(new string [] {"sháo", "Jingfei" }));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "zhang", "ailing", "eileen", "chang", "ai-ling", "shi nai'an", "luo guanzhong", "wu cheng'en", "cao xueqin" };
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayThreeShaoZhang", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayThreeShaoReply", subject));
        }
      }
    }

    private void AddOmegaReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().ContainsAny(new string[] { "omega" }));
      if (searchResult.Count() > 0)
      {
        bool infiltrator = db.GetHackingProgression(UserName) == HackingProgression.Infiltrator;
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "oskar", "jönsson"};
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          if (infiltrator)
          {
            db.InterMails.Add(MakeMail("DayThreeOmegaOskarInfiltrator", "Re: " + detailedResult.First().Subject));
          }
          else
          {
            db.InterMails.Add(MakeMail("DayThreeOmegaOskarNoInfiltrator", "Re: " + detailedResult.First().Subject));
          }
        }
        else
        {
          if (infiltrator)
          {
            db.InterMails.Add(MakeMail("DayThreeOmegaInfiltratorReply", subject));
          }
          else
          {
            db.InterMails.Add(MakeMail("DayThreeOmegaNoInfiltratorReply", subject));
          }
        }
      }
    }

    private void AddOskarReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("oskar"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "omega", "forwarded message"};
        var detailedResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAll(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("DayThreeOskarOmega", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("DayThreeOskarReply", subject));
        }
      }
    }

    private void AddSandraReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("sandra"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("DayThreeSandraReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddVitalyReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("vitaly"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("DayThreeVitalyReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddReplies()
    {
      var sentMails = db.SentMails.Where(r => r.UserName == UserName && r.Date == "1").ToList();
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
      newReports.Add(MakeReport("DayThreeMailBrightfieldCarbon"));
      newReports.Add(MakeReport("DayThreePhoneKinsingerBrightfield"));
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
      if (reviewed.Contains("DayTwoMailKinsingerVelika") || reviewed.Contains("DayTwoPhoneKinsingerVelika"))
      {
        results++;
        newReports.Add(MakeReport("DayThreeVisionsOfTheFuture"));
        newReports.Add(MakeReport("DayThreePhoneKinsingerVelika"));
      }
      if (reviewed.Contains("DayTwoPhoneKinsingerOmega"))
      {
        results++;
        newReports.Add(MakeReport("DayThreeSurveillanceBlack"));
        newReports.Add(MakeReport("DayThreeMailBlackUnknown"));
      }
      if (reviewed.Contains("DayTwoMailKinsingerCarpenter"))
      {
        results++;
        newReports.Add(MakeReport("DayThreeMailKinsingerVelika"));
        newReports.Add(MakeReport("DayThreeSurveillanceStudentMeeting"));
        db.NewsItems.Add(MakeNewsItem("Students"));
        if (!reviewed.Contains("DayTwoMailKinsingerVelika"))
        {
          newReports.Add(MakeReport("DayThreePhoneKinsingerVelika"));
        }
      }
      if (reviewed.Contains("DayTwoSurveillanceStudentMeeting"))
      {
        if (!reviewed.Contains("DayTwoMailKinsingerCarpenter"))
        {
          results++;
          db.NewsItems.Add(MakeNewsItem("Students"));
          newReports.Add(MakeReport("DayThreeSurveillanceStudentMeeting"));
        }
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
      if (results > 0)
      {
        newMails.Add(MakeMail("DayThreeVedeninFound"));
      }
      else
      {
        newMails.Add(MakeMail("DayThreeVedeninNone"));
      }
      if (flaggedCount > maxReviewed)
      {
        newMails.Add(MakeMail("DayThreeVedeninMany"));
      }
      bool infiltrator = db.GetHackingProgression(UserName) == HackingProgression.Infiltrator;
      if (infiltrator)
      {
        newMails.Add(MakeMail("DayThreeOmegaInfiltrator"));
      }
      else
      {
        newMails.Add(MakeMail("DayThreeOmegaNoInfiltrator"));
        newMails.Add(MakeMail("DayThreeAlphaNoInfiltrator"));
      }

      var potluckEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(0)).Single();
      var potluckContribution = potluckEvent.Contribution.ToLower();
      var bear = db.GetBearType(UserName).ToLower();
      bool bearParty = potluckContribution.ContainsAny(new string[] { "bear", bear });
      if (!potluckEvent.Attending)
      {
        newMails.Add(MakeMail("DayThreeSalvinuNoShow"));
      }
      else if (potluckContribution.Count() == 0 || potluckContribution.Contains(" evil"))
      {
        newMails.Add(MakeMail("DayThreeSalvinuContrib"));
      }
      else if (bearParty)
      {
        newMails.Add(MakeMail("DayThreeSalvinuGrizzly"));
      }
      else if (potluckContribution.Contains("spatula"))
      {
        newMails.Add(MakeMail("DayThreeSalvinuSpatula"));
      }
      else
      {
        newMails.Add(MakeMail("DayThreeSalvinuContrib"));
      }

      var onboardingEvent = db.SocialEvents.ToList().Where(x => x.UserName == UserName && x.Date == db.GetDateString(1)).Single();
      var onboardingContribution = onboardingEvent.Contribution.ToLower();
      var bearBoarding = onboardingContribution.ContainsAny(new string[] { "bear", bear });
      if (!onboardingEvent.Attending)
      {
        newMails.Add(MakeMail("DayThreeBenedettoNoShow"));
      }
      else if (onboardingContribution.Count() == 0 || onboardingContribution.Contains("cheerful"))
      {
        newMails.Add(MakeMail("DayThreeBenedettoExpectedContrib"));
      }
      else if (bearBoarding)
      {
        if (bearParty)
        {
          newMails.Add(MakeMail("DayThreeBenedettoDoubleGrizzly"));
        }
        else
        {
          newMails.Add(MakeMail("DayThreeBenedettoGrizzly"));
        }
      }
      else
      {
        newMails.Add(MakeMail("DayThreeBenedettoDifferentContrib"));
      }

      //Sandra mails
      if (results > 0)
      {
        newMails.Add(MakeMail("DayThreeSandraResults"));
      }
      else
      {
        newMails.Add(MakeMail("DayThreeSandraNoResults"));
      }

      bool deletedSuspicious = !extantReports.Exists(x => x.Name == "DayTwoMailKinsingerVelika") ||
        !extantReports.Exists(x => x.Name == "DayTwoPhoneKinsingerOmega") ||
        !extantReports.Exists(x => x.Name == "DayTwoMailKinsingerCarpenter") ||
        !extantReports.Exists(x => x.Name == "DayTwoMailKinsingerCarpenter");

      //Shao mails
      if (results >= 3)
      {
        newMails.Add(MakeMail("DayThreeShaoAllResults"));
      }
      else if (results > 0)
      {
        if (!deletedSuspicious)
        {
          newMails.Add(MakeMail("DayThreeShaoResults"));
        }
        else
        {
          newMails.Add(MakeMail("DayThreeShaoResultsDeletedSuspicious"));
        }
      }
      else
      {
        if (!deletedSuspicious)
        {
          newMails.Add(MakeMail("DayThreeShaoNoResults"));
        }
        else
        {
          newMails.Add(MakeMail("DayThreeShaoNoResultsDeletedSuspicious"));
        }
      }

      newMails.ForEach(s => db.InterMails.Add(s));
    }

    private void AddNewsItems()
    {
      var newsItems = new List<NewsItem>
          {
              MakeNewsItem("Synergy")
          };
      newsItems.ForEach(x => db.NewsItems.Add(x));
    }

    private void UpdateSocialEvents()
    {
      var onboarding = db.SocialEvents.Where(x => x.UserName == UserName && x.Title.ToLower().Contains("onboarding")).Single();
      onboarding.Enabled = false;
      db.Entry(onboarding).State = EntityState.Modified;
      var socialEvents = new List<SocialEvent>
          {
            MakeEvent("Team club night")
          };
      socialEvents.ForEach(x => db.SocialEvents.Add(x));
    }

    private void EndGameWithArrest()
    {
      var gameState = db.GameStates.Where(s => s.UserName == UserName).Single();
      gameState.GameProgression = GameProgression.Arrested;
      db.Entry(gameState).State = EntityState.Modified;

      List<InterMail> interMails = db.InterMails.Where(m => m.UserName == UserName).ToList();
      interMails.ForEach(s => db.InterMails.Remove(s));

      List<Report> reports = db.Reports.Where(m => m.UserName == UserName).ToList();
      reports.ForEach(r => db.Reports.Remove(r));

      db.NewsItems.Add(MakeNewsItem("ArrestedDeletedAll"));

      db.InterMails.Add(MakeMail("SandraArrestedDeletedAll"));
      db.InterMails.Add(MakeMail("VedeninArrested"));

      var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
      var sentMail = db.SentMails.Where(s => s.UserName == UserName).ToList();
      bool informedOskar = sentMail.Any(s => s.GetContents().ToLower().ContainsAny(searchTerms));
      if (informedOskar)
      {
        db.InterMails.Add(MakeMail("OskarArrestedDeletedAllMail"));
      }
      else
      {
        db.InterMails.Add(MakeMail("OskarArrestedDeletedAll"));
      }
      db.SaveChanges();
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName && x.Day == 1).ToList();
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