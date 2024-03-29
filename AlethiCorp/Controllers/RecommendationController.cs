﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.DAL;
using AlethiCorp.Models;

namespace AlethiCorp.Controllers
{
  public class RecommendationController : InternalLayoutController
  {
    // GET: Recommendations
    public ActionResult Index()
    {
      if (db.GetDay(User.Identity.Name) != 3)
      {
        return RedirectToAction("Unavailable");
      }
      ViewBag.DroneStrike = db.GetHackingProgression(User.Identity.Name) == HackingProgression.Concurrency;
      return View(db.Recommendations.Where(x => x.UserName == User.Identity.Name).ToList());
    }

    // GET: Recommendations/Create
    public ActionResult Create()
    {
      if (db.GetDay(User.Identity.Name) != 3)
      {
        return RedirectToAction("Unavailable");
      } 
      ViewBag.DroneStrike = db.GetHackingProgression(User.Identity.Name) == HackingProgression.Concurrency;
      return View();
    }

    public ActionResult NameSuggestions(string term)
    {
      var names = new List<String>();
      names.Add("Adam Underwood");
      names.Add("Benedetto Tornincasa");
      names.Add("Sandra Silvern");
      names.Add("Oskar Jönsson");
      names.Add("Alex DuMaurier");
      names.Add("Andrea Schueler");
      names.Add("Vitaly Vedenin");
      names.Add("Sháo Jingfei");
      names.Add("Salvinu Manduca");

      names.Add("Martin Brightfield");
      names.Add("Patricia Carbon");
      names.Add("Silva Carpenter");
      names.Add("Alyona Artemieva");
      names.Add("Alex Jaspers");
      names.Add("John Blue");
      names.Add("Cédric Kinsinger");
      names.Add("Velika Dožić");
      names.Add("Adroushan Gasparyan");
      names.Add("John Compass");
      names.Add("Samuel Compass");
      names.Add("Hannah Abendroth");
      names.Add("Absolon Martineau");
      names.Add("Victor Marian");
      names.Add("Philip Passeri");
      string bearType = db.GetBearType(User.Identity.Name);
      names.Add(bearType);

      var suggestions = names.Where(r => r.ToLower().Contains(term.ToLower()));
      return Json(suggestions, JsonRequestBehavior.AllowGet);
    }

    // POST: Recommendations/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,ThreatLevel,ThreatType,DroneStrike,Comments")] Recommendation recommendation)
    {
      recommendation.UserName = User.Identity.Name;
      if (ModelState.IsValid)
      {
        db.Recommendations.Add(recommendation);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(recommendation);
    }

    // GET: Recommendations/Edit/5
    public ActionResult Edit(int? id)
    {
      if (db.GetDay(User.Identity.Name) != 3)
      {
        return RedirectToAction("Unavailable");
      } 
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Recommendation recommendation = db.Recommendations.Find(id);
      if (recommendation == null)
      {
        return HttpNotFound();
      }
      ViewBag.DroneStrike = db.GetHackingProgression(User.Identity.Name) == HackingProgression.Concurrency;
      return View(recommendation);
    }

    // POST: Recommendations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,ThreatLevel,ThreatType,DroneStrike,Comments")] Recommendation recommendation)
    {
      recommendation.UserName = User.Identity.Name;
      if (ModelState.IsValid)
      {
        db.Entry(recommendation).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(recommendation);
    }

    // POST: Recommendations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Recommendation recommendation = db.Recommendations.Find(id);
      //Recommendation can be null if the button is clicked twice in close succession
      if (recommendation != null)
      {
        db.Recommendations.Remove(recommendation);
        db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Unavailable()
    {
      if (db.IsEmployee(User.Identity.Name))
      {
        var personalInfo = db.PersonalInfos.Where(r => r.UserName == User.Identity.Name).Single();
        ViewBag.PlayerName = personalInfo.FirstName + " " + personalInfo.LastName;
        ViewBag.BearType = db.GetBearType(User.Identity.Name);
      }

      return View();
    }
  }
}
