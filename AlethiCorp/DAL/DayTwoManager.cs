using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
  public class DayTwoManager : DayManager
  {
    public DayTwoManager(DatabaseContext db, string userName)
      : base(db, userName)
    { }

    private readonly int maxReviewed = 3;

    private void AddAlexReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("alex"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("AlexReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddAndreaReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("andrea"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "malcolm", "jurassic", "dinosaur" };
        var detailedResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("AndreaMalcolm", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("AndreaReply", subject));
        }
      }
    }

    private void AddBenedettoReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("benedetto"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "haka", "facilitator", "underwood" };
        var detailedResult = searchResult.Where(s => s.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("BenedettoHaka", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("BenedettoReply", subject));
        }
      }
    }

    private void AddOmegaReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("omega"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("OmegaReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddOskarReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("oskar"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "hacker", "omega", "alpha", "iam" };
        var detailedResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("OskarHackers", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("OskarReply", subject));
        }
      }
    }

    private void AddSandraReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("sandra"));
      if (searchResult.Count() > 0)
      {
        var subject = "Re: " + searchResult.First().Subject;
        var searchTerms = new string[] { "haka", "facilitator", "underwood" };
        var detailedResult = searchResult.Where(r => r.GetContents().ToLower().ContainsAny(searchTerms));
        if (detailedResult.Count() > 0)
        {
          db.InterMails.Add(MakeMail("SandraHaka", "Re: " + detailedResult.First().Subject));
        }
        else
        {
          db.InterMails.Add(MakeMail("SandraReply", subject));
        }
      }
    }

    private void AddVitalyReplies(List<SentMail> sentMails)
    {
      var searchResult = sentMails.Where(r => r.Recipient.ToLower().Contains("vitaly"));
      if (searchResult.Count() > 0)
      {
        db.InterMails.Add(MakeMail("VitalyReply", "Re: " + searchResult.First().Subject));
      }
    }

    private void AddReplies()
    {
      var sentMails = db.SentMails.Where(r => r.UserName == UserName && r.Date == "0").ToList();
      if (sentMails.Count() == 0)
      {
        return;
      }

      AddAlexReplies(sentMails);
      AddAndreaReplies(sentMails);
      AddBenedettoReplies(sentMails);
      AddOmegaReplies(sentMails);
      AddOskarReplies(sentMails);
      AddSandraReplies(sentMails);
      AddVitalyReplies(sentMails);
    }

    private void AddUnconditionalReports()
    {
      var reports = new List<Report>
          {
            MakeReport("DayTwoPamphlets"),
            MakeReport("DayTwoPhoneKinsingerVelika"),
            MakeReport("DayTwoSurveillanceKinsinger"),
            MakeReport("DayTwoMailKinsingerVelika"),
            MakeReport("DayTwoMailKinsingerCarpenter"),
            MakeReport("DayTwoPhoneKinsingerOmega"),
            MakeReport("DayTwoPhoneBrightfieldMartineau")
          };
      reports.ForEach(r => db.Reports.Add(r));
    }

    private bool AddReports(List<Report> extantReports, List<Report> flaggedReports)
    {
      AddUnconditionalReports();
      var newReports = new List<Report>();
      bool results = false;
      bool deletedCorrect = !extantReports.Exists(x => x.Name == "DayOnePhoneBrightfieldCarbon");
      var reviewed = new List<string>();
      foreach (var report in flaggedReports.SelectRandomSubset(maxReviewed))
      {
        reviewed.Add(report.Name);
      }

      if (!deletedCorrect)
      {
        results = true;
        newReports.Add(MakeReport("DayTwoSurveillanceCarbon"));
        newReports.Add(MakeReport("DayTwoSurveillanceStudentMeeting"));
        newReports.Add(MakeReport("DayTwoMailCarbonCarpenter"));
      }
      if (reviewed.Contains("DayOneMailBrightfieldCarbon"))
      {
        results = true;
        newReports.Add(MakeReport("DayTwoSurveillanceBrightfieldCarbon"));
        if (!newReports.Exists(x => x.Name == "DayTwoMailCarbonCarpenter"))
        {
          newReports.Add(MakeReport("DayTwoMailCarbonCarpenter"));
        }
      }
      if (reviewed.Contains("DayOnePhoneBrightfieldCompass"))
      {
        results = true;
        newReports.Add(MakeReport("DayTwoSurveillanceBrightfieldCompass"));
        newReports.Add(MakeReport("DayTwoSurveillanceBrightfieldCompassConversation"));
        newReports.Add(MakeReport("DayTwoMailSamuelJohn"));
      }
      if (reviewed.Contains("DayOnePhoneCompassAbendroth"))
      {
        results = true;
        newReports.Add(MakeReport("DayTwoSurveillanceAbendroth"));
        newReports.Add(MakeReport("DayTwoBananas"));
      }
      newReports.ForEach(x => db.Reports.Add(x));
      return results;
    }

    private void AddInterMails(List<Report> extantReports, List<Report> flaggedReports, bool results)
    {
      AddReplies();
      var extantCount = extantReports.Count();

      bool deletedCorrect = !extantReports.Exists(x => x.Name == "DayOnePhoneBrightfieldCarbon");

      bool flaggedCorrect = flaggedReports.Exists(x => x.Name == "DayOnePhoneBrightfieldCarbon");
      int flaggedCount = flaggedReports.Count;

      var newMails = new List<InterMail>();
      if (results)
      {
        newMails.Add(MakeMail("VedeninFound"));
        newMails.Add(MakeMail("OmegaSnitch"));
      }
      else if (flaggedCount > 0)
      {
        newMails.Add(MakeMail("VedeninNone"));
        newMails.Add(MakeMail("OmegaSurprised"));
      }
      else if (extantCount == 0)
      {
        newMails.Add(MakeMail("OmegaDeletedAll"));
      }
      else if (deletedCorrect)
      {
        newMails.Add(MakeMail("OmegaNothingFlaggedDeletedSuspicious"));
      }
      else
      {
        newMails.Add(MakeMail("OmegaSurprisedNoFound"));
      }
      if (flaggedCount > maxReviewed)
      {
        newMails.Add(MakeMail("VedeninMany"));
      }

      var socialEvent = db.SocialEvents.Where(x => x.UserName == UserName).Single();
      var contribution = socialEvent.Contribution.ToLower();
      var bear = db.PersonalityTests.Where(x => x.UserName == UserName).Single().BearType.ToLower();
      bool bearParty = contribution.ContainsAny(new string[] { "bear", bear });
      if (!socialEvent.Attending)
      {
        newMails.Add(MakeMail("BenedettoNoShow"));
      }
      else if (contribution.Count() == 0 || contribution.Contains(" evil"))
      {
        newMails.Add(MakeMail("BenedettoNoContrib"));
      }
      else if (bearParty)
      {
        newMails.Add(MakeMail("BenedettoGrizzly"));

      }
      else if (contribution.Contains("spatula"))
      {
        newMails.Add(MakeMail("BenedettoSpatula"));
      }
      else
      {
        newMails.Add(MakeMail("BenedettoContrib"));
      }

      //Sandra's mails
      if (flaggedCorrect)
      {
        newMails.Add(MakeMail("SandraCorrectFlagged"));
      }
      else if (extantCount == 0)
      {
        newMails.Add(MakeMail("SandraDeletedAll"));
      }
      else if (flaggedCount == 0)
      {
        if (deletedCorrect)
        {
          newMails.Add(MakeMail("SandraNothingFlaggedDeletedSuspicious"));
        }
        else
        {
          newMails.Add(MakeMail("SandraNothingFlagged"));
        }
      }
      else
      {
        if (deletedCorrect)
        {
          newMails.Add(MakeMail("SandraSomethingFlaggedDeletedSuspicious"));
        }
        else
        {
          newMails.Add(MakeMail("SandraSomethingFlagged"));
        }
      }

      if (bearParty)
      {
        newMails.Add(MakeMail("ShaoBear"));
      }
      else
      {
        newMails.Add(MakeMail("ShaoGreeting"));
      }

      newMails.ForEach(s => db.InterMails.Add(s));
    }

    private void AddNewsItems()
    {
      var newsItems = new List<NewsItem>
          {
              MakeNewsItem("SiChrisConsulting")
          };
      newsItems.ForEach(x => db.NewsItems.Add(x));
    }

    private void UpdateSocialEvents()
    {
      var potluck = db.SocialEvents.Where(x => x.UserName == UserName && x.Title.Contains("potluck")).Single();
      potluck.Enabled = false;
      db.Entry(potluck).State = EntityState.Modified;
      var socialEvents = new List<SocialEvent>
          {
            MakeEvent("Onboarding event")
          };
      socialEvents.ForEach(x => db.SocialEvents.Add(x));
    }

    public override void ActivateDay()
    {
      var extantReports = db.Reports.Where(x => x.UserName == UserName).ToList();
      var flaggedReports = extantReports.Where(x => x.Flagged).ToList();

      bool results = AddReports(extantReports, flaggedReports);

      AddInterMails(extantReports, flaggedReports, results);

      AddNewsItems();

      UpdateSocialEvents();

      db.SaveChanges();
    }
  }
}