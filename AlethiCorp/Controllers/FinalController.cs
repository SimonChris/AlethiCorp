using AlethiCorp.DAL;
using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
  [Authorize]
  public class FinalController : Controller
  {
    protected DatabaseContext db = new DatabaseContext();

    // GET: Final
    public ActionResult Index()
    {
      if (db.GetProgression(User.Identity.Name) == GameProgression.Ongoing)
      {
        return HttpNotFound();
      }
      return View();
    }

    public ActionResult Bear()
    {
      if (db.GetProgression(User.Identity.Name) != GameProgression.Arrested || !db.BearEnabled(User.Identity.Name))
      {
        return HttpNotFound();
      }

      db.ReleaseBear(User.Identity.Name);
      return RedirectToAction("Index", "Internal");
    }
  }
}