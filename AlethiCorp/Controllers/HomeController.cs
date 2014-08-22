using AlethiCorp.DAL;
using AlethiCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlethiCorp.Controllers
{
    public class HomeController : HomeLayoutController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Omega = db.GetDay(User.Identity.Name) == 1;
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Values()
        {
            return View();
        }

        public ActionResult Careers()
        {
            if (db.NeedsRegistration(User.Identity.Name))
            {
                return RedirectToAction("Register", "PersonalityTest");
            }
            return View();
        }

        public ActionResult Services()
        {
            var services = new List<string>();
            if (db.GetProgression(User.Identity.Name) != GameProgression.Comply)
            {
              services.Add("Business Information Management");
              services.Add("Public Information Management");
              services.Add("Private Information Management");

              var serviceWordsOne = new List<string>();
              var serviceWordsTwo = new List<string>();
              var serviceWordsThree = new List<string>();

              serviceWordsOne.Add("Cloud");
              serviceWordsOne.Add("Service");
              serviceWordsOne.Add("Application");
              serviceWordsOne.Add("Social");
              serviceWordsOne.Add("Supply");
              serviceWordsOne.Add("Big");
              serviceWordsOne.Add("Green");
              serviceWordsOne.Add("Procurement");
              serviceWordsOne.Add("Mobile");
              serviceWordsOne.Add("Social");
              serviceWordsOne.Add("Infrastructure");
              serviceWordsOne.Add("Testing");
              serviceWordsOne.Add("Business");
              serviceWordsOne.Add("Customer");
              serviceWordsOne.Add("Business");
              serviceWordsOne = serviceWordsOne.Shuffle();

              serviceWordsTwo.Add("Synergy");
              serviceWordsTwo.Add("Integration");
              serviceWordsTwo.Add("Lifecycle");
              serviceWordsTwo.Add("Business");
              serviceWordsTwo.Add("Chain");
              serviceWordsTwo.Add("Data");
              serviceWordsTwo.Add("Application");
              serviceWordsTwo.Add("Testing");
              serviceWordsTwo.Add("Solution");
              serviceWordsTwo.Add("Media");
              serviceWordsTwo.Add("Service");
              serviceWordsTwo.Add("Services & Analytics");
              serviceWordsTwo.Add("Service");
              serviceWordsTwo.Add("Experience");
              serviceWordsTwo.Add("Process");
              serviceWordsTwo = serviceWordsTwo.Shuffle();

              serviceWordsThree.Add("Analysis");
              serviceWordsThree.Add("Management");
              serviceWordsThree.Add("Analytics");
              serviceWordsThree.Add("Testing");
              serviceWordsThree.Add("Outsourcing");
              serviceWordsThree.Add("Consulting");
              serviceWordsThree.Add("Outsourcing");
              serviceWordsThree.Add("Integration");
              serviceWordsThree.Add("Consulting");
              serviceWordsThree.Add("Management");
              serviceWordsThree.Add("Solutions");
              serviceWordsThree.Add("Integration");
              serviceWordsThree.Add("Consulting");
              serviceWordsThree.Add("Solutions");
              serviceWordsThree.Add("Analytics");
              serviceWordsThree = serviceWordsThree.Shuffle();

              for (int i = 0; i < 15; i++)
              {
                services.Add(serviceWordsOne[i] + " " + serviceWordsTwo[i] + " " + serviceWordsThree[i]);
              }

              services = services.Shuffle();
            }
            else
            {
              for (int i = 0; i < 18; i++)
              {
                services.Add("Information Acquisition");
              }
            }

            return View(services);
        }
        

        [Authorize]
        public ActionResult ApplyNotQualified()
        {
            return View();
        }

        [Authorize]
        public ActionResult ApplyConsultant()
        {
            if (db.NeedsRegistration(User.Identity.Name))
            {
                return RedirectToAction("Register", "PersonalityTest");
            }
            return View();
        }
    }
}
