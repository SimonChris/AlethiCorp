using AlethiCorp.DAL;
using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
  public class HomeLayoutController : Controller
  {
    protected DatabaseContext db = new DatabaseContext();

    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      ViewBag.IsEmployee = db.IsEmployee(User.Identity.Name);
      var progression = db.GetProgression(User.Identity.Name);
      ViewBag.Comply = progression == GameProgression.Comply;
      ViewBag.BearBearBear = progression == GameProgression.BearBearBear;
      ViewBag.BearType = db.GetBearType(User.Identity.Name);
    }

    protected override void Dispose(bool disposing)
    {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}