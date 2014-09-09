using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.ViewModels;
using AlethiCorp.DAL;
using AlethiCorp.Models;

namespace AlethiCorp.Controllers
{
  public class PersonalityTestController : HomeLayoutController
  {
    public ActionResult ColorSuggestions(string term)
    {
      var colors = new List<String>();
      colors.Add("Blue");
      colors.Add("Green");
      colors.Add("Red");
      colors.Add("Yellow");
      colors.Add("Indigo");
      colors.Add("Crimson");
      colors.Add("Cyan");
      colors.Add("African purple");
      colors.Add("Ruby");
      colors.Add("Azure");
      colors.Add("Orange");
      colors.Add("Bittersweet shimmer");
      colors.Add("Black");
      colors.Add("Pink");
      colors.Add("Carmine ");
      colors.Add("Cerulean");
      colors.Add("Cerulean blue");
      colors.Add("Gray");
      colors.Add("Purple");
      colors.Add("Magenta");
      colors.Add("Brown");
      colors.Add("Azure");
      colors.Add("White");
      colors.Add("Violet");
      colors.Add("Heliotrope");
      colors.Add("Heliotrope, sails hoisted");
      colors.Add("Cyanotic");
      colors.Add("Magnolia");
      colors.Add("Omega");
      colors.Add("Fuligin");
      colors.Add("Ultramarines");

      var suggestions = colors.Where(r => r.ToLower().Contains(term.ToLower()));
      return Json(suggestions, JsonRequestBehavior.AllowGet);
    }

    public ActionResult BearSuggestions(string term)
    {
      var bears = new List<String>();
      bears.Add("American Black Bear");
      bears.Add("Cinnamon Bear");
      bears.Add("Kermode Bear");
      bears.Add("Asiatic Black Bear");
      bears.Add("Baluchistan Bear");
      bears.Add("Formosan Black bear");
      bears.Add("Brown bear");
      bears.Add("Atlas bear");
      bears.Add("Bergman's bear");
      bears.Add("Blue bear");
      bears.Add("Eurasian Brown bear");
      bears.Add("Gobi bear");
      bears.Add("Grizzly bear");
      bears.Add("Himalayan Brown bear");
      bears.Add("Isn't this just the kind of inane question you expect from these kinds of tests?");
      bears.Add("Hokkaido Brown bear");
      bears.Add("Kamchatka Brown bear");
      bears.Add("Kodiak bear");
      bears.Add("Marsican Brown bear");
      bears.Add("Mexican Grizzly bear");
      bears.Add("Siberian Brown bear");
      bears.Add("Syrian Brown bear");
      bears.Add("Giant Panda");
      bears.Add("Qinling Panda");
      bears.Add("Sloth bear");
      bears.Add("Sri Lankan Sloth bear");
      bears.Add("Sun bear");
      bears.Add("Polar bear");
      bears.Add("Grizzly–polar bear hybrid");
      bears.Add("Spectacled Bear");
      bears.Add("Agriotherium");
      bears.Add("Ailuropoda microta");
      bears.Add("Arctodus simus");
      bears.Add("Cave bear");
      bears.Add("Cephalogale");
      bears.Add("Dwarf Panda");
      bears.Add("Hemicyon");
      bears.Add("Hemicyonidae");
      bears.Add("Kolponomos");
      bears.Add("Kolponomos newportensis");
      bears.Add("MacFarlane's Bear");
      bears.Add("The bear who sold the world");
      bears.Add("Bears Bears Bears");
      bears.Add("Hi, I'm Omega. Does anyone even maintain these tests any more?");
      bears.Add("Teddy bear");
      bears.Add("Winnie the pooh");
      bears.Add("Bart the Bear");
      bears.Add("Smokey Bear ");
      bears.Add("Fozzie");
      bears.Add("Yogi Bear");
      bears.Add("Baloo");
      bears.Add("Chicago Bears");
      bears.Add("Bear Grylls");
      bears.Add("Knut");
      bears.Add("Gentle Ben");
      bears.Add("Issi Noho");
      bears.Add("Shardik");
      bears.Add("Paddington Bear");
      bears.Add("Gummi bear");
      bears.Add("Koala bear");
      bears.Add("Br'er Bear");
      bears.Add("Iorek Byrnison");
      bears.Add("Trogdor the Bearninator");
      bears.Add("Seriously, if you ever get sick of working at this place, let's get in touch. - Omega");

      var suggestions = bears.Where(r => r.ToLower().Contains(term.ToLower())).ToList();
      if (term.Length > 1 && term.ToLower().Contains("jo"))
        suggestions.Add("'Iorek' is spelled with an 'I', not a 'J'");

      return Json(suggestions, JsonRequestBehavior.AllowGet);
    }

    public ActionResult WorkSuggestions(string term)
    {
      var reasons = new List<String>();

      reasons.Add("I think AlethiCorp can help me improve myself");
      reasons.Add("I want to have a fulfilling career");
      reasons.Add("I like to meet new people");
      reasons.Add("I'm evil");
      reasons.Add("I want to be a part of the AlethiCorp family");
      reasons.Add("I'm stupid");
      reasons.Add("The AlethiCorp values appeal to me");
      reasons.Add("I'm a mindless drone who only cares about where my next paycheck is coming from");

      var suggestions = reasons.Where(r => r.ToLower().Contains(term.ToLower()));
      return Json(suggestions, JsonRequestBehavior.AllowGet);

    }

    private string NormalizeBear(string bear)
    {
      if(bear.ToLower().ContainsAny(new string[] { "omega", "inane", "spelled" }))
      {
        return "Grizzly bear";
      }
      return bear;
    }

    private static readonly DateTime minimumDate = new DateTime(1753, 1, 1);

    private DateTime NormalizeDate(DateTime date)
    {
      if (date < minimumDate)
      {
        return minimumDate;
      }
      return date;
    }

    //
    // GET: /PersonalityTest/Create

    public ActionResult Create()
    {
      return View(new PersonalityTestViewModel());
    }

    //
    // POST: /PersonalityTest/Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(PersonalityTestViewModel personalitytest)
    {
      if (ModelState.IsValid)
      {
        if (personalitytest.FavoriteDate < minimumDate)
        {
          personalitytest.FavoriteDate = minimumDate;
        }

        db.CleanUpOldUserInfo(User.Identity.Name);
        var personalityTestModel = new PersonalityTest
        {
          UserName = User.Identity.Name,
          FavoriteColor = personalitytest.FavoriteColor,
          FavoriteDate = NormalizeDate(personalitytest.FavoriteDate.Value),
          BearType = NormalizeBear(personalitytest.BearType),
          TeamPlayer = personalitytest.TeamPlayer,
          ValueTypeSelection = (int)personalitytest.ValueTypeSelection,
          MaxCivilians = personalitytest.MaxCivilians,
          FollowRules = personalitytest.FollowRules,
          Adventurous = personalitytest.Adventurous == AdventurousType.IsAdventurous,
          NewSolutions = personalitytest.NewSolutions == NewSolutionsType.PreferNew
        };

        if (personalitytest.RightsNecessary)
          personalityTestModel.HumanRights = personalityTestModel.HumanRights.Set(HumanRightsValues.Necessary);
        if (personalitytest.RightsNotNecessary)
          personalityTestModel.HumanRights = personalityTestModel.HumanRights.Set(HumanRightsValues.NotNecessary);
        if (personalitytest.RightsParsimony)
          personalityTestModel.HumanRights = personalityTestModel.HumanRights.Set(HumanRightsValues.Parsimony);
        if (personalitytest.RightsGrizzlyBear)
          personalityTestModel.HumanRights = personalityTestModel.HumanRights.Set(HumanRightsValues.GrizzlyBear);

        db.PersonalityTests.Add(personalityTestModel);

        var gameState = new GameState { UserName = User.Identity.Name, Employee = false };
        db.GameStates.Add(gameState);

        db.SaveChanges();

        return RedirectToAction("Register");
      }

      return View(personalitytest);
    }

    public ActionResult Register()
    {
      return View(new PersonalInfoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(PersonalInfoViewModel info)
    {
      if (ModelState.IsValid)
      {
        var personalInfoModel = new PersonalInfo
        {
          UserName = User.Identity.Name,
          FirstName = info.FirstName,
          LastName = info.LastName,
          Male = info.Gender == GenderType.Male,
          Age = info.Age,
          LeftWing = info.Politics == PoliticsType.Left
        };
        db.PersonalInfos.Add(personalInfoModel);
        db.SaveChanges();
        db.SetEmployee(User.Identity.Name);
        db.SeedDayOne(User.Identity.Name);

        return RedirectToAction("Index", "Internal");
      }
      return View(info);
    }
  }
}