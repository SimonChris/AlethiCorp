using AlethiCorp.Models;
using AlethiCorp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlethiCorp.DAL
{
  public abstract class DayManager : IDayManager
  {
    protected DatabaseContext db;

    protected string UserName { get; set; }

    protected readonly List<ReportViewModel> reportList = JsonConvert.DeserializeObject<List<ReportViewModel>>(
    System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/Reports.json"));

    public DayManager(DatabaseContext db, string userName)
    {
      this.db = db;
      UserName = userName;
    }

    protected InterMail MakeMail(string name, string subject = null)
    {
      return new InterMail
      {
        UserName = UserName,
        Name = name,
        Subject = subject
      };
    }

    protected SocialEvent MakeEvent(string title)
    {
      return new SocialEvent
      {
        UserName = UserName,
        Title = title,
        Date = db.GetDateString(db.GetDay(UserName)),
        Enabled = true,
        Contribution = ""
      };
    }

    protected Course MakeCourse(string title)
    {
      return new Course
      {
        UserName = UserName,
        Title = title,
        Completed = false,
        Grade = ""
      };
    }

    protected NewsItem MakeNewsItem(string name)
    {
      return new NewsItem
      {
        UserName = UserName,
        Name = name
      };
    }

    protected Report MakeReport(string name)
    {
      return new Report
      {
        UserName = UserName,
        Name = name,
        Day = Convert.ToInt32(reportList.Find(x => x.Name == name).Date)
      };
    }

    protected void CachePlaythroughInformation(string ending)
    {
      var personalInfo = db.PersonalInfos.Where(r => r.UserName == UserName).Single();
      db.Playthroughs.Add(new Playthrough
      {
        UserName = UserName,
        PlayerName = personalInfo.FirstName + " " + personalInfo.LastName,
        Ending = ending,
        HackingProgression = db.GetHackingProgression(UserName),
        FavoriteColor = db.GetFavoriteColor(UserName),
        BearType = db.GetBearType(UserName),
        EkstraInfo = ""
      });
    }

    public abstract void ActivateDay();
  }
}