using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.Models;
using AlethiCorp.DAL;
using Newtonsoft.Json;
using AlethiCorp.ViewModels;
using System.Text.RegularExpressions;

namespace AlethiCorp.Controllers
{
  public class InterMailController : InternalLayoutController
  {
    //
    // GET: /InterMail/

    public ActionResult Index()
    {
      List<InterMail> intermails = db.InterMails.Where(x => x.UserName == User.Identity.Name).ToList();
      intermails.Reverse();
      ViewBag.Count = intermails.Count;

      var mailList = JsonConvert.DeserializeObject<List<InterMailViewModel>>(
      System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/InterMails.json"));
      var finalList = new List<InterMailViewModel>();
      foreach (var mail in intermails)
      {
        var viewMail = mailList.Find(x => x.Name == mail.Name);
        viewMail.Id = mail.Id;
        viewMail.Read = mail.Read;
        viewMail.Date = db.GetDateString(Convert.ToInt32(viewMail.Date));
        if (mail.Subject != null)
        {
          viewMail.Subject = mail.Subject;
        }
        finalList.Add(viewMail);
      }

      return View(finalList);
    }

    //
    // GET: /InterMail/Details/5

    public ActionResult Details(int id = 0)
    {
      InterMail intermail = db.InterMails.Find(id);
      if (intermail == null)
      {
        intermail = new InterMail() { Name = "DefaultMail", Read = true };
      }
      if (!intermail.Read)
      {
        intermail.Read = true;
        db.Entry(intermail).State = EntityState.Modified;
        db.SaveChanges();
      }

      ViewBag.Enabled = intermail.Name != "DefaultMail";
      ViewBag.ComplyMail = intermail.Name == "VedeninArrested";

      var mailList = JsonConvert.DeserializeObject<List<InterMailViewModel>>(
      System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/InterMails.json"));
      var intermailViewModel = mailList.Where(x => x.Name == intermail.Name).Single();
      intermailViewModel.Id = intermail.Id;
      if (intermail.Subject != null)
      {
        intermailViewModel.Subject = intermail.Subject;
      }
      intermailViewModel.Message = db.GetHTMLString(User.Identity.Name, intermailViewModel.Message);
      return View(intermailViewModel);
    }


    //
    // GET: /InterMail/Delete/5

    public ActionResult Delete(int id = 0)
    {
      InterMail intermail = db.InterMails.Find(id);
      if (intermail == null)
      {
        return HttpNotFound();
      }
      return View(intermail);
    }

    //
    // POST: /InterMail/Delete/5

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      InterMail intermail = db.InterMails.Find(id);
      db.InterMails.Remove(intermail);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Sent mails

    public ActionResult NameSuggestions(string term)
    {
      var names = new List<String>();
      names.Add("Adam Underwood");
      names.Add("Benedetto Tornincasa");
      names.Add("Omega");
      names.Add("Sandra Silvern");
      names.Add("Alpha");
      names.Add("Oskar Jönsson");
      names.Add("Alex DuMaurier");
      names.Add("Andrea Schueler");
      names.Add("Vitaly Vedenin");

      int day = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single().Day;
      if(day > 0)
      {
        names.Add("Sháo Lingfei");
      }
      if(day > 1)
      {
        names.Add("Salvinu Manduca");
      }

      var suggestions = names.Where(r => r.ToLower().Contains(term.ToLower()));
      return Json(suggestions, JsonRequestBehavior.AllowGet);
    }

    //
    // GET: /InterMail/Create

    private Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

    public ActionResult Create(int? id, bool forward = false)
    {
      ViewBag.Forward = forward;
      if (id != null)
      {
        InterMail intermail = db.InterMails.Find(id);
        if (intermail == null)
        {
          return HttpNotFound();
        }
        var mailList = JsonConvert.DeserializeObject<List<InterMailViewModel>>(
        System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/InterMails.json"));

        var mailDetails = mailList.Where(x => x.Name == intermail.Name).Single();
        var sentMail = new SentMail();
        if (!forward)
        {
          sentMail.Recipient = mailDetails.Author;
          sentMail.Subject = "Re: " + mailDetails.Subject;
        }
        else
        {
          sentMail.Subject = "Fwd: " + ( mailDetails.Subject ?? intermail.Subject );
          var message = db.GetHTMLString(User.Identity.Name, mailDetails.Message);
          sentMail.Message = "---Forwarded Message---\r\n" + htmlRegex.Replace(message, string.Empty);
        }

        return View(sentMail);
      }
      return View();
    }

    //
    // POST: /InterMail/Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(SentMail sentMail)
    {
      sentMail.UserName = User.Identity.Name;
      if (ModelState.IsValid)
      {
        sentMail.Date = db.GetDay(User.Identity.Name).ToString();
        db.SentMails.Add(sentMail);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(sentMail);
    }

    public ActionResult Sent()
    {
      List<SentMail> sentMails = db.SentMails.Where(x => x.UserName == User.Identity.Name).ToList();
      sentMails.Reverse();
      ViewBag.Count = sentMails.Count;
      foreach (var mail in sentMails)
      {
        mail.Date = db.GetDateString(Convert.ToInt32(mail.Date));
      }
      return View(sentMails);
    }

    public ActionResult SentDetails(int id = 0)
    {
      SentMail sentMail = db.SentMails.Find(id);
      if (sentMail == null)
      {
        return HttpNotFound();
      }
      if (!sentMail.Read)
      {
        sentMail.Read = true;
        db.Entry(sentMail).State = EntityState.Modified;
        db.SaveChanges();
      }
      return View(sentMail);
    }
  }
}