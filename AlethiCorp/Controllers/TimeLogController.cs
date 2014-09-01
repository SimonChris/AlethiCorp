using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.Models;
using AlethiCorp.DAL;

namespace AlethiCorp.Controllers
{
  public class TimeLogController : InternalLayoutController
  {
    //
    // GET: /TimeLog/Create
    public ActionResult Create()
    {
      return View();
    }

    //
    // POST: /TimeLog/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TimeLogViewModel timelogviewmodel)
    {
      if (ModelState.IsValid)
      {
        if (db.GetDay(User.Identity.Name) > 2)
        {
          int droneStrikes = db.Recommendations.Where(r => r.UserName == User.Identity.Name && r.DroneStrike).Count();
          if (droneStrikes > 0)
          {
            return RedirectToAction("DroneStrikeConfirmation");
          }
        }
        db.IncrementDay(User.Identity.Name);
        return RedirectToAction("Index", "Internal");
      }

      return View(timelogviewmodel);
    }

    public ActionResult DroneStrikeConfirmation()
    {
      int droneStrikes = db.Recommendations.Where(r => r.UserName == User.Identity.Name && r.DroneStrike).Count();
      return View(droneStrikes);
    }

    [HttpPost, ActionName("DroneStrikeConfirmation")]
    [ValidateAntiForgeryToken]
    public ActionResult DroneStrikesConfirmed()
    {
      db.IncrementDay(User.Identity.Name);
      return RedirectToAction("Index", "Internal");
    }

    public ActionResult TimeLogSuggestions(string hours)
    {
      var numbers = new List<String>();
      numbers.Add("8");

      return Json(numbers, JsonRequestBehavior.AllowGet);
    }
  }
}