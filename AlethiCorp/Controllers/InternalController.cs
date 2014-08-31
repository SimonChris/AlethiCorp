using AlethiCorp.DAL;
using AlethiCorp.Models;
using AlethiCorp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
    public class InternalController : InternalLayoutController
    {
        public ActionResult Index()
        {
            ViewBag.IsEmployee = db.IsEmployee(User.Identity.Name);

            var newsItems = db.NewsItems.Where(x => x.UserName == User.Identity.Name).ToList();
            newsItems.Reverse();
            newsItems = newsItems.Take(3).ToList();

            var newsList = JsonConvert.DeserializeObject<List<NewsItemViewModel>>(
            System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/NewsItems.json"));
            var finalList = new List<NewsItemViewModel>();
            foreach (var item in newsItems)
            {
              var newsItem = newsList.Find(x => x.Name == item.Name);
              newsItem.HeadLine = db.GetDateString(Convert.ToInt32(newsItem.Date)) + ": " + newsItem.HeadLine;
              newsItem.MainText = db.GetHTMLString(User.Identity.Name, newsItem.MainText);
              finalList.Add(newsItem);
            }
            return View(finalList);
        }

        public ActionResult InterMail()
        {
            return View();
        }

        public ActionResult University()
        {
            return View();
        }

        public ActionResult Social()
        {
            return View();
        }

        public ActionResult Information()
        {
            return View();
        }

        public ActionResult Comply()
        {
          var state = db.GameStates.Where(s => s.UserName == User.Identity.Name).SingleOrDefault();
          if(state == null || state.GameProgression == GameProgression.Ongoing)
          {
            return HttpNotFound();
          }
          if (state.GameProgression != GameProgression.Comply)
          {
            state.GameProgression = GameProgression.Comply;
            db.Entry(state).State = EntityState.Modified;
            db.SaveChanges();
          }
          return View();
        }

        public ActionResult Reset()
        {
          db.CleanUpOldUserInfo(User.Identity.Name, false);
          db.SeedDayOne(User.Identity.Name);
          return RedirectToAction("Index", "Internal");
        }

        public ActionResult ResetAll()
        {
          db.CleanUpOldUserInfo(User.Identity.Name);
          return RedirectToAction("Index", "Home");
        }
    }
}
