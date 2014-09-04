using AlethiCorp.DAL;
using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
    [Authorize]
    public class InternalLayoutController : Controller
    {
        protected DatabaseContext db = new DatabaseContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
          ViewBag.InterMailCount = db.GetInterMailCountString(User.Identity.Name);
          var progression = db.GetProgression(User.Identity.Name);
          ViewBag.Ongoing = progression == GameProgression.Ongoing;
          ViewBag.Arrested = progression == GameProgression.Arrested;
          ViewBag.Comply = progression == GameProgression.Comply;
          ViewBag.Success = progression == GameProgression.Success;
          ViewBag.Career = progression == GameProgression.Career;
          ViewBag.BearReleased = progression == GameProgression.Bear;
          ViewBag.BearBearBear = progression == GameProgression.BearBearBear;

          ViewBag.Recommendation = db.GetDay(User.Identity.Name) == 3;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}