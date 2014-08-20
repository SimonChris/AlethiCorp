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
    public class PersonDataController : HomeLayoutController
    {
        //
        // GET: /PersonData/

        public ActionResult Index()
        {
            return View(db.PersonDatas.ToList());
        }

        //
        // GET: /PersonData/Details/5

        public ActionResult Details(int id = 0)
        {
            PersonData persondata = db.PersonDatas.Find(id);
            if (persondata == null)
            {
                return HttpNotFound();
            }
            return View(persondata);
        }

        //
        // GET: /PersonData/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PersonData/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonData persondata)
        {
            if (ModelState.IsValid)
            {
                db.PersonDatas.Add(persondata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persondata);
        }

        //
        // GET: /PersonData/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PersonData persondata = db.PersonDatas.Find(id);
            if (persondata == null)
            {
                return HttpNotFound();
            }
            return View(persondata);
        }

        //
        // POST: /PersonData/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonData persondata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persondata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persondata);
        }

        //
        // GET: /PersonData/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PersonData persondata = db.PersonDatas.Find(id);
            if (persondata == null)
            {
                return HttpNotFound();
            }
            return View(persondata);
        }

        //
        // POST: /PersonData/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonData persondata = db.PersonDatas.Find(id);
            db.PersonDatas.Remove(persondata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}