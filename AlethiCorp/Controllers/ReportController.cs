using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.Models;
using AlethiCorp.ViewModels;
using AlethiCorp.DAL;
using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;

namespace AlethiCorp.Controllers
{
  public class ReportController : ReportLayoutController
  {
    //
    // GET: /Report/

    public ActionResult Front()
    {
      return View();
    }

    private List<ReportViewModel> GetReports(ReportType type)
    {
      var reports = db.Reports.Where(x => (x.UserName == User.Identity.Name)).ToList();
      reports.Reverse();
      var reportViewModels = new List<ReportViewModel>();
      foreach (var report in reports)
      {
        var viewReport = reportList.Find(x => x.Name == report.Name && x.Type == type);
        if (viewReport != null)
        {
          viewReport.Id = report.Id;
          viewReport.Read = report.Read;
          viewReport.Flagged = report.Flagged;
          viewReport.Date = db.GetDateString(Convert.ToInt32(viewReport.Date));
          viewReport.Title = db.ReplaceTextTokens(User.Identity.Name, viewReport.Title);
          reportViewModels.Add(viewReport);
        }
      }
      return reportViewModels;
    }

    //
    // GET: /Report/
    public ActionResult Reports()
    {
      ViewBag.Title = "Reports";
      return View("Index", GetReports(ReportType.Report));
    }
    //
    // GET: /Report/
    public ActionResult EMail()
    {
      ViewBag.Title = "E-Mail Conversations";
      return View("Index", GetReports(ReportType.EMail));
    }
    //
    // GET: /Report/
    public ActionResult Phone()
    {
      ViewBag.Title = "Phone Conversations";
      return View("Index", GetReports(ReportType.Phone));
    }
    //
    // GET: /Report/
    public ActionResult Surveillance()
    {
      ViewBag.Title = "Surveillance";
      return View("Index", GetReports(ReportType.Surveillance));
    }

    //
    // GET: /Report/Details/5

    public ActionResult Details(int id = 0)
    {
      Report Report = db.Reports.Find(id);
      if (Report == null)
      {
        return RedirectToAction("Warning");
      }
      var viewReport = reportList.Find(x => x.Name == Report.Name);
      ViewBag.Type = (int)viewReport.Type;

      if (!Report.Read)
      {
        Report.Read = true;
        db.Entry(Report).State = EntityState.Modified;
        db.SaveChanges();
      }

      viewReport.RowVersion = Report.RowVersion;
      viewReport.Flagged = Report.Flagged;
      viewReport.Date = db.GetDateString(Convert.ToInt32(viewReport.Date));
      viewReport.Title = db.ReplaceTextTokens(User.Identity.Name, viewReport.Title);
      viewReport.Information = db.GetHTMLString(User.Identity.Name, viewReport.Information);
      return View(viewReport);
    }

    private ActionResult RedirectToType(ReportType type)
    {
      switch (type)
      {
        case ReportType.Report:
          return RedirectToAction("Reports");
        case ReportType.EMail:
          return RedirectToAction("EMail");
        case ReportType.Phone:
          return RedirectToAction("Phone");
        case ReportType.Surveillance:
          return RedirectToAction("Surveillance");
        default:
          return RedirectToAction("Reports");
      }
    }

    //
    // POST:  /Report/Details/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Details(ReportViewModel viewData)
    {
      Report Report = db.Reports.Find(viewData.Id);
      if (Report == null)
      {
        return RedirectToAction("Warning");
      }
      if (!Report.RowVersion.SequenceEqual(viewData.RowVersion))
      {
        var gameState = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single();
        if (gameState.HackingProgression == HackingProgression.Infiltrator)
        {
          gameState.HackingProgression = HackingProgression.Concurrency;
          db.Entry(gameState).State = EntityState.Modified;
          db.SaveChanges();
        }
        return RedirectToAction("Concurrency");
      }
      Report.Flagged = viewData.Flagged;
      db.Entry(Report).State = EntityState.Modified;
      int changes = db.SaveChanges();

      var viewReport = reportList.Find(x => x.Name == Report.Name);
      return RedirectToType(viewReport.Type);
    }

    public ActionResult Concurrency()
    {
      var hackingProgression = db.GameStates.Where(s => s.UserName == User.Identity.Name).Single().HackingProgression;
      ViewBag.Infiltrator = hackingProgression == HackingProgression.Infiltrator;
      ViewBag.Concurrency = hackingProgression == HackingProgression.Concurrency;

      return View();
    }

    public ActionResult Warning()
    {
      return View();
    }

    //
    // GET: /Report/Create

    public ActionResult Create()
    {
      return View();
    }

    //
    // POST: /Report/Delete/5

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Report Report = db.Reports.Find(id);
      db.Reports.Remove(Report);
      db.SaveChanges();

      var viewReport = reportList.Find(x => x.Name == Report.Name);
      return RedirectToType(viewReport.Type);
    }
  }
}