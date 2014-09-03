using AlethiCorp.DAL;
using AlethiCorp.Models;
using AlethiCorp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
  [Authorize]
  public class ReportLayoutController : Controller
  {
    protected DatabaseContext db = new DatabaseContext();

    protected readonly List<ReportViewModel> reportList = JsonConvert.DeserializeObject<List<ReportViewModel>>(
    System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/Reports.json"));

    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      ViewBag.ReportCount = db.GetItemCountString(User.Identity.Name, (int)ReportType.Report, reportList);
      ViewBag.EMailCount = db.GetItemCountString(User.Identity.Name, (int)ReportType.EMail, reportList);
      ViewBag.PhoneCount = db.GetItemCountString(User.Identity.Name, (int)ReportType.Phone, reportList);
      ViewBag.SurveillanceCount = db.GetItemCountString(User.Identity.Name, (int)ReportType.Surveillance, reportList);
      ViewBag.Flagging = db.GetDay(User.Identity.Name) < 3;
      var progression = db.GetProgression(User.Identity.Name);
      ViewBag.BearEnding = progression == GameProgression.Bear || progression == GameProgression.BearBearBear;
    }

    protected override void Dispose(bool disposing)
    {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}