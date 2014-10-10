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
      if(progression == GameProgression.Arrested || progression == GameProgression.Comply)
      {
        return View("Arrested");
      }
      else if(progression == GameProgression.Career)
      {
        return View("Success");
      }
      else if(progression == GameProgression.Bear || progression == GameProgression.BearBearBear)
      {
        return View("BearEnding");
      }
      else if(progression == GameProgression.Andrea)
      {
        return View("AndreaEnding");
      }
      return View("ManualEnding");
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

    public ActionResult Andrea()
    {
      var progression = db.GetProgression(User.Identity.Name);
      if ((progression != GameProgression.Arrested && progression != GameProgression.Comply) || !db.AndreaImpressed(User.Identity.Name))
      {
        return HttpNotFound();
      }

      db.JoinAndrea(User.Identity.Name);
      return RedirectToAction("Index", "Internal");
    }

    public ActionResult Amusing()
    {
      var progression = db.GetProgression(User.Identity.Name);
      if (progression == GameProgression.Ongoing)
      {
        return View("ManualAmusing");
      }

      ViewBag.BearTypeLower = db.GetBearType(User.Identity.Name).ToLower();
      ViewBag.BearType = db.GetBearType(User.Identity.Name);
      ViewBag.BearTypeCapital = db.GetBearType(User.Identity.Name).ToUpper();
      return View();
    }
  }
}