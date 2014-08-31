using System;
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
      ViewBag.DroneStrike = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single().HackingProgression == HackingProgression.Concurrency;
      return View(db.Recommendations.Where(x => x.UserName == User.Identity.Name).ToList());
    }

    // GET: Recommendations/Create
    public ActionResult Create()
    {
      ViewBag.DroneStrike = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single().HackingProgression == HackingProgression.Concurrency;
      return View();
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
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Recommendation recommendation = db.Recommendations.Find(id);
      if (recommendation == null)
      {
        return HttpNotFound();
      }
      ViewBag.DroneStrike = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single().HackingProgression == HackingProgression.Concurrency;
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
      db.Recommendations.Remove(recommendation);
      db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
