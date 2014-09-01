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
using AlethiCorp.ViewModels;

namespace AlethiCorp.Controllers
{
    public class UniversityController : InternalLayoutController
    {
        // GET: University
        public ActionResult Index()
        {
            return View(db.Courses.Where(x => x.UserName == User.Identity.Name).ToList());
        }

        // GET: University/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: University/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: University/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Title,Completed,Grade")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.UserName = User.Identity.Name;
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: University/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Haka", new { page = 0 });
        }

        public ActionResult Haka(int? page)
        {
          ViewBag.Day = db.GetDay(User.Identity.Name);
          var hackingProgression = db.GetHackingProgression(User.Identity.Name);
          ViewBag.Infiltrator = hackingProgression == HackingProgression.Infiltrator;
          ViewBag.Concurrency = hackingProgression == HackingProgression.Concurrency;
          return View(page);
        }

        // POST: University/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Title,Completed,Grade")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: University/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: University/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: University/Exam
        public ActionResult HakaExam()
        {
            return View();
        }

        Random rng = new Random();

        // POST: University/HakaExam
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HakaExam([Bind(Include = "Id,UserName,AnswerOne,AnswerTwo,AnswerThree")] HakaExam hakaExam)
        {
            if (ModelState.IsValid)
            {
                var hakaCourse = db.Courses.Where(x => x.UserName == User.Identity.Name && x.Title.ToLower().Contains("haka")).Single();
                var grade = rng.Next(100);
                hakaCourse.Completed = true;
                hakaCourse.Grade = grade.ToString() + "/100";
                hakaCourse.Answer = hakaExam.AnswerOne + "\r\n" + hakaExam.AnswerTwo + "\r\n" + hakaExam.AnswerThree;
                db.Entry(hakaCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HakaResult");
            }

            return View(hakaExam);
        }

        public ActionResult HakaResult()
        {
            var hakaCourse = db.Courses.Where(x => x.UserName == User.Identity.Name && x.Title.ToLower().Contains("haka")).Single();
            return View((object) hakaCourse.Grade);
        }
    }
}
