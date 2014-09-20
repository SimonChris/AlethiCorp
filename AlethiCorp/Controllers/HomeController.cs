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
      var progression = db.GetProgression(User.Identity.Name);
      if (progression == GameProgression.Comply)
      {
        for (int i = 0; i < 18; i++)
        {
          services.Add("Information Acquisition");
        }
      }
      else if(progression == GameProgression.BearBearBear)
      {
        services.Add("American Black Bear");
        services.Add("Cinnamon Bear");
        services.Add("Kermode Bear");
        services.Add("Asiatic Black Bear");
        services.Add("Baluchistan Bear");
        services.Add("Formosan Black bear");
        services.Add("Brown bear");
        services.Add("Atlas bear");
        services.Add("Bergman's bear");
        services.Add("Blue bear");
        services.Add("Eurasian Brown bear");
        services.Add("Gobi bear");
        services.Add("Grizzly bear");
        services.Add("Himalayan Brown bear");
        services.Add("Hokkaido Brown bear");
        services.Add("Kamchatka Brown bear");
        services.Add("Kodiak bear");
        services.Add("Marsican Brown bear");
        services.Add("Mexican Grizzly bear");
        services.Add("Siberian Brown bear");
        services.Add("Syrian Brown bear");
        services.Add("Giant Panda");
        services.Add("Qinling Panda");
        services.Add("Sloth bear");
        services.Add("Sri Lankan Sloth bear");
        services.Add("Sun bear");
        services.Add("Polar bear");
        services.Add("Grizzly–polar bear hybrid");
        services.Add("Spectacled Bear");
        services.Add("Agriotherium");
        services.Add("Ailuropoda microta");
        services.Add("Arctodus simus");
        services.Add("Cave bear");
        services.Add("Cephalogale");
        services.Add("Dwarf Panda");
        services.Add("Hemicyon");
        services.Add("Hemicyonidae");
        services.Add("Kolponomos");
        services.Add("Kolponomos newportensis");
        services.Add("MacFarlane's Bear");
        services.Add("The bear who sold the world");
        services.Add("Bears Bears Bears");
        services.Add("Teddy bear");
        services.Add("Winnie the pooh");
        services.Add("Bart the Bear");
        services.Add("Smokey Bear ");
        services.Add("Fozzie");
        services.Add("Yogi Bear");
        services.Add("Baloo");
        services.Add("Chicago Bears");
        services.Add("Bear Grylls");
        services.Add("Knut");
        services.Add("Gentle Ben");
        services.Add("Issi Noho");
        services.Add("Shardik");
        services.Add("Paddington Bear");
        services.Add("Gummi bear");
        services.Add("Koala bear");
        services.Add("Br'er Bear");
        services.Add("Iorek Byrnison");
        services.Add("Trogdor the Bearninator");
      }
      else
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

    public ActionResult DumpAllText()
    {
      var mailList = JsonConvert.DeserializeObject<List<InterMailViewModel>>(
      System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/InterMails.json"));

      var reportList = JsonConvert.DeserializeObject<List<ReportViewModel>>(
      System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/Reports.json"));

      var newsList = JsonConvert.DeserializeObject<List<NewsItemViewModel>>(
      System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "Messages/NewsItems.json"));

      var allText = "";
      allText += "<h3>InterMails</h3>";
      mailList.ForEach(m => allText += db.GetHTMLString("", m.Message) + "</br>");
      allText += "<h3>Reports</h3>";
      reportList.ForEach(r => allText += db.GetHTMLString("", r.Information) + "</br>");
      allText += "<h3>News</h3>";
      newsList.ForEach(n => allText += db.GetHTMLString("", n.MainText) + "</br>");
      ViewBag.AllText = allText;

      return View();
    }
  }
}
