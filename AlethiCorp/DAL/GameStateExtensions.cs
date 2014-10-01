using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using AlethiCorp.ViewModels;
using Newtonsoft.Json;

namespace AlethiCorp.DAL
{
  //DatabaseContext extension methods for complex game state retrieval and updates
  public static class GameStateExtensions
  {
    private static string ReplaceMailInfo(this DatabaseContext db, string userName, string text)
    {
      var reviewMail = db.SentMails.Where(r => r.UserName == userName &&
        r.Date == "2" && r.Recipient.ToLower().Contains("andrea")).First();

      text = text.Replace("MAILSUBJECT", reviewMail.Subject);
      text = text.Replace("MAILBODY", reviewMail.Message);

      return text;
    }

    public static string ReplaceTextTokens(this DatabaseContext db, string userName, string text)
    {
      var personalInfo = db.PersonalInfos.Where(r => r.UserName == userName).Single();

      text = text.Replace("FIRSTNAME", personalInfo.FirstName);
      text = text.Replace("LASTNAME", personalInfo.LastName);
      text = text.Replace("OBJECTPRONOUN", personalInfo.Male ? "him" : "her");
      text = text.Replace("PRONOUN", personalInfo.Male ? "he" : "she");
      text = text.Replace("POSSESSIVE", personalInfo.Male ? "his" : "her");
      text = text.Replace("TITLE", personalInfo.Male ? "Mr." : "Miss");
      text = text.Replace("GENDERREF", personalInfo.Male ? "guy" : "girl");
      text = text.Replace("DANCEPARTNER", personalInfo.Male ? "Sandra" : "Vita");
      text = text.Replace("BEARTYPE", db.GetBearType(userName));
      text = text.Replace("FAVORITECOLOR", db.GetFavoriteColor(userName));

      if (text.ContainsAny(new string[] { "MAILSUBJECT", "MAILBODY" }))
      {
        text = db.ReplaceMailInfo(userName, text);
      }

      return text;
    }

    public static string GetHTMLString(this DatabaseContext db, string userName, string directory)
    {
      string html = File.ReadAllText(HttpRuntime.AppDomainAppPath + directory);
      return userName == "" ? html : db.ReplaceTextTokens(userName, html);
    }

    public static void CleanUpOldUserInfo(this DatabaseContext db, string userName, bool includeEmployment = true)
    {
      if (includeEmployment)
      {
        //Remove redundant user information
        var oldTest = db.PersonalityTests.Find(userName);
        if (oldTest != null)
          db.PersonalityTests.Remove(oldTest);

        var oldState = db.GameStates.Find(userName);
        if (oldState != null)
          db.GameStates.Remove(oldState);

        var oldInfo = db.PersonalInfos.Find(userName);
        if (oldInfo != null)
          db.PersonalInfos.Remove(oldInfo);
      }
      else
      {
        var oldState = db.GameStates.Find(userName);
        if (oldState != null)
        {
          oldState.Day = 0;
          oldState.GameProgression = GameProgression.Ongoing;
          oldState.HackingProgression = HackingProgression.Innocent;
          db.Entry(oldState).State = EntityState.Modified;
        }
      }

      var interMails = db.InterMails.Where(x => x.UserName == userName).ToList();
      interMails.ForEach(x => db.InterMails.Remove(x));

      var sentMails = db.SentMails.Where(x => x.UserName == userName).ToList();
      sentMails.ForEach(x => db.SentMails.Remove(x));

      var socialEvents = db.SocialEvents.Where(x => x.UserName == userName).ToList();
      socialEvents.ForEach(x => db.SocialEvents.Remove(x));

      var courses = db.Courses.Where(x => x.UserName == userName).ToList();
      courses.ForEach(x => db.Courses.Remove(x));

      var newsItems = db.NewsItems.Where(x => x.UserName == userName).ToList();
      newsItems.ForEach(x => db.NewsItems.Remove(x));

      var reports = db.Reports.Where(x => x.UserName == userName).ToList();
      reports.ForEach(x => db.Reports.Remove(x));

      var recommendations = db.Recommendations.Where(x => x.UserName == userName).ToList();
      recommendations.ForEach(x => db.Recommendations.Remove(x));

      db.SaveChanges();
    }

    private static IDayManager MakeDayManager(DatabaseContext db, string userName, int day)
    {
      switch (day)
      {
        case 0:
          return new DayOneManager(db, userName);
        case 1:
          return new DayTwoManager(db, userName);
        case 2:
          return new DayThreeManager(db, userName);
        case 3:
          return new DayFourManager(db, userName);
        case 4:
          return new DayFiveManager(db, userName);
        default:
          throw new ArgumentException("Day " + (day + 1).ToString() + " has not been implemented.");
      }
    }

    public static void SeedDayOne(this DatabaseContext db, string userName)
    {
      MakeDayManager(db, userName, 0).ActivateDay();
    }

    public static void IncrementDay(this DatabaseContext db, string userName)
    {
      GameState gameState = db.GameStates.Where(r => r.UserName == userName).SingleOrDefault();
      if (gameState == null)
        return;

      gameState.Day++;
      db.Entry(gameState).State = EntityState.Modified;
      db.SaveChanges();

      MakeDayManager(db, userName, gameState.Day).ActivateDay();
    }

    public static void ReleaseBear(this DatabaseContext db, string userName)
    {
      var dayFiveManager = MakeDayManager(db, userName, 4) as DayFiveManager;
      dayFiveManager.ReleaseBear();
    }

    public static void JoinAndrea(this DatabaseContext db, string userName)
    {
      var dayFiveManager = MakeDayManager(db, userName, 4) as DayFiveManager;
      dayFiveManager.JoinAndrea();
    }

    public static string GetContents(this SentMail sentMail)
    {
      return sentMail.Subject + sentMail.Message;
    }

    public static int GetDay(this DatabaseContext db, string userName)
    {
      GameState gameState = db.GameStates.Where(r => r.UserName == userName).SingleOrDefault();
      if (gameState == null)
        return 0;
      return gameState.Day;
    }

    public static string GetDateString(this DatabaseContext db, string userName)
    {
      return db.GetDateString(db.GetDay(userName));
    }

    public static string GetDateString(this DatabaseContext db, int day)
    {
      switch (day)
      {
        case -1:
          return "April 19";
        case 0:
          return "April 20";
        case 1:
          return "April 21";
        case 2:
          return "April 22";
        case 3:
          return "April 23";
        case 4:
          return "April 24";
        default:
          throw new ArgumentException("Day " + (day + 1).ToString() + " has not yet been implemented.");
      }
    }


    public static bool NeedsRegistration(this DatabaseContext db, string userName)
    {
      GameState gameState = db.GameStates.Where(r => r.UserName == userName).SingleOrDefault();
      if (gameState == null)
        return false;

      return !gameState.Employee;
    }

    public static void SetEmployee(this DatabaseContext db, string userName)
    {
      GameState gameState = db.GameStates.Where(r => r.UserName == userName).SingleOrDefault();
      if (gameState == null)
        return;

      gameState.Employee = true;
      db.Entry(gameState).State = EntityState.Modified;
      db.SaveChanges();
    }

    private static int GetItemCount(this DatabaseContext db, string userName, int type, List<ReportViewModel> reportList)
    {
      var unreadReports = db.Reports.Where(r => r.UserName == userName && !r.Read);
      int count = 0;
      foreach (var report in unreadReports)
      {
        if ((int)reportList.Find(x => x.Name == report.Name).Type == type)
        {
          count++;
        }
      }
      return count;
    }

    public static bool IsEmployee(this DatabaseContext db, string userName)
    {
      GameState gameState = db.GameStates.Where(r => r.UserName == userName).SingleOrDefault();
      if (gameState == null)
        return false;

      return gameState.Employee;
    }

    public static string GetItemCountString(this DatabaseContext db, string userName, int type, List<ReportViewModel> reportList)
    {
      string itemCountString = "";
      int count = db.GetItemCount(userName, type, reportList);
      if (count > 0)
      {
        itemCountString += " (" + count + ")";
      }
      return itemCountString;
    }

    public static string GetInterMailCountString(this DatabaseContext db, string userName)
    {
      string mailCountString = "";
      int count = db.InterMails.Where(r => r.UserName == userName && !r.Read).Count();
      if (count > 0)
      {
        mailCountString += " (" + count + ")";
      }
      return mailCountString;
    }

    public static GameProgression GetProgression(this DatabaseContext db, string userName)
    {
      var gameState = db.GameStates.Where(s => s.UserName == userName).SingleOrDefault();
      return gameState != null ? gameState.GameProgression : GameProgression.Ongoing;
    }

    public static HackingProgression GetHackingProgression(this DatabaseContext db, string userName)
    {
      var gameState = db.GameStates.Where(s => s.UserName == userName).SingleOrDefault();
      return gameState != null ? gameState.HackingProgression : HackingProgression.Innocent;
    }

    public static string GetRawBearType(this DatabaseContext db, string userName)
    {
      return db.PersonalityTests.Where(x => x.UserName == userName).Single().BearType;;
    }

    public static string GetBearType(this DatabaseContext db, string userName)
    {
      var bearType = db.GetRawBearType(userName);
      if (bearType.ToLower().ContainsAny(new string[] { "omega", "inane", "spelled" }))
      {
        return "Grizzly bear";
      }
      return bearType;
    }

    public static string GetFavoriteColor( this DatabaseContext db, string userName ) {
      return db.PersonalityTests.Where( x => x.UserName == userName ).Single().FavoriteColor;
    }

    public static bool BearEnabled(this DatabaseContext db, string userName)
    {
      var bear = db.GetBearType(userName).ToLower();
      int bearCount = db.SocialEvents.Where(s => s.UserName == userName).ToList().Count(s => s.Contribution.ToLower().ContainsAny(new string[] { "bear", bear }));

      return bearCount > 1;
    }

    public static bool AndreaImpressed(this DatabaseContext db, string userName)
    {
      var andreaMails = db.SentMails.Where(r => r.UserName == userName && r.Recipient.ToLower().Contains("andrea")).ToList();
      bool andreaQuestion = andreaMails.Where(r => r.Date == "2" && r.GetContents().ToLower().ContainsAny(new string[] { "nellie", " bly", "nelly" })).Any();
      bool andreaAnswer = andreaMails.Where(r => r.Date == "3" && r.GetContents().ToLower().ContainsAll(new string[] { "gasp", " laugh" })).Any();
      return andreaQuestion && andreaAnswer;
    }

    public static bool ContainsAny(this string text, string[] terms)
    {
      return terms.Any(t => text.Contains(t));
    }

    public static bool ContainsAll(this string text, string[] terms)
    {
      return terms.All(t => text.Contains(t));
    }

    private static Random rng = new Random();

    //Fisher–Yates shuffle
    public static List<T> Shuffle<T>(this List<T> list)
    {
      var result = new List<T>(list);
      int n = result.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        T value = result[k];
        result[k] = result[n];
        result[n] = value;
      }
      return result;
    }

    public static IEnumerable<T> SelectRandomSubset<T>(this List<T> list, int count)
    {
      count = Math.Min(count, list.Count());
      var result = list.Shuffle();
      for (int i = 0; i < count; i++)
      {
        yield return result[i];
      }
    }
  }
}