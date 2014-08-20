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
          var gameState = db.GameStates.Where(s => s.UserName == User.Identity.Name).SingleOrDefault();
          if(gameState == null || gameState.GameProgression == GameProgression.Ongoing)
          {
            return HttpNotFound();
          }
          return View();
        }
    }
}