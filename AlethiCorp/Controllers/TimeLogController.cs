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
            if (db.GetDay(User.Identity.Name) > 2)
            {
                return RedirectToAction("Final");
            }
            if (ModelState.IsValid)
            {
                //db.TimeLogViewModels.Add(timelogviewmodel);
                //db.SaveChanges();
                db.IncrementDay(User.Identity.Name);
                return RedirectToAction("Index", "Internal");
            }

            return View(timelogviewmodel);
        }

        public ActionResult TimeLogSuggestions(string hours)
        {
            var numbers = new List<String>();
            numbers.Add("8");

            return Json(numbers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Final()
        {
          return View();
        }
    }
}