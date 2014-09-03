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
      var progression = db.GetProgression(User.Identity.Name);
      if (progression == GameProgression.Ongoing)
      {
        return HttpNotFound();
      }
      if(progression == GameProgression.Arrested || progression == GameProgression.Comply)
      {
        return View("Arrested");
      }
      else if(progression == GameProgression.Bear || progression == GameProgression.BearBearBear)
      {
        return View("BearEnding");
      }
      return View("Arrested");
    }

    public ActionResult Bear()
    {
      var progression = db.GetProgression(User.Identity.Name);
      if ((progression != GameProgression.Arrested && progression != GameProgression.Comply) || !db.BearEnabled(User.Identity.Name))
      {
        return HttpNotFound();
      }

      db.ReleaseBear(User.Identity.Name);
      return RedirectToAction("Index", "Internal");
    }
  }
}