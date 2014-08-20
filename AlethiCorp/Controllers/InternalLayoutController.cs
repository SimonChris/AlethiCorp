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
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}