using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlethiCorp.DAL;
using AlethiCorp.Models;

namespace AlethiCorp.Controllers
{
    public class SocialController : InternalLayoutController
    {
        public ActionResult PotluckSuggestions(string term)
        {
            var recipes = new List<String>();
            recipes.Add("Apple pie");
            recipes.Add("Grain salad");
            recipes.Add("Fresh fruit");
            recipes.Add("Deviled eggs");
            recipes.Add("Chocolate cake");
            recipes.Add("Waldorf salad");
            recipes.Add("Pasta salad");
            recipes.Add("Berry Cobbler");
            recipes.Add("Chocolate Peanut Butter Bon Bons");
            recipes.Add("Cocktail weenies");
            recipes.Add("Potato salad");
            recipes.Add("Guacamole");
            recipes.Add("During tomato season, salsa is cheap, easy, and so delicious");
            recipes.Add("Beer and cheese dip");
            recipes.Add("Tuna salad");
            recipes.Add("Barley. Just barley");
            recipes.Add("Risotto");
            recipes.Add("Roasted beets with cheese");
            recipes.Add("Cucumber finger sandwiches");
            recipes.Add("Sliced pita and hummus");
            recipes.Add("Prosciutto-wrapped melon slices");
            recipes.Add("Caviar");
            recipes.Add("Truffle-oil roasted vegetables");
            recipes.Add("Apples and caramel dip");
            recipes.Add("Grilled pears and mascarpone");
            recipes.Add("Edible gold Leaf");
            recipes.Add("A spatula");
            recipes.Add("Mediterranean turkey meatballs with herbed yogurt sauce");
            recipes.Add("Homemade bread");
            recipes.Add("Cinnamon walnut dulce de leche bars");
            recipes.Add("Meyer lemon grain salad with asparagus, almonds and goat cheese");
            recipes.Add("Welsh rarebit");
            recipes.Add("Rice balls, or 'onigiri'");
            recipes.Add("Couscous with apricot");
            recipes.Add("Honey wheat rolls");
            recipes.Add("Caramelized onion and bacon tart");
            recipes.Add("Chicken parmesan casserole");
            recipes.Add("Pork tenderloin with mustard sauce");
            recipes.Add("Lentil ratatouille");
            recipes.Add("Jumbo shrimp cocktail");
            recipes.Add("Banana cake");
            recipes.Add("Oriental coleslaw");
            recipes.Add("Blueberry-almond bars");
            recipes.Add("Mexican polenta pie");
            recipes.Add("Don't you realize these people are evil? Don't feed the evil! - Alpha");
            var bearType = db.GetBearType(User.Identity.Name);
            recipes.Add(bearType);

            var suggestions = recipes.Where(r => r.ToLower().Contains(term.ToLower()));
            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnboardingSuggestions(string term)
        {
            var contributions = new List<String>();
            contributions.Add("A cheerful mood and a sunny disposition");

            return Json(contributions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClubSuggestions(string term)
        {
          var suggestions = new List<String>();

          bool infiltrator = db.GetHackingProgression(User.Identity.Name) == HackingProgression.Infiltrator;

          if (infiltrator && term.Length > 1 && term.ToLower().Contains("jack daniel"))
          {
            suggestions.Add(@"Ok, listen up. There is a security vulnerability in the database, but as an HR guy
            I don't have the necessary access to exploit it. 
            This is where you come in. Open a report in two different browser windows/tabs simultaneously, and try to flag
            it from both of them. This should trigger the vulnerability. 
            Omega will slip through during the window of opportunity. Good luck.
            Oh, and I really shouldn't have to say this, but please don't enter this text into the contribution
            field. Choose a drink of some kind :)");
          }
          else if(term.ToLower().Contains("beer"))
          {
            suggestions.Add("Yes, but what kind of beer?");
          }
          else if(term.ToLower().Contains("heineken"))
          {
            suggestions.Add("Heineken? Fuck that shit! Pabst Blue Ribbon!");
          }
          else
          {
            var drinks = new List<String>();
            drinks.Add("White Russian");
            drinks.Add("Black Russian");

            var color = db.PersonalityTests.Where(x => x.UserName == User.Identity.Name).Single();
            drinks.Add(color.FavoriteColor + " Russian");

            drinks.Add("Manhattan");
            drinks.Add("Strawberry Daiquiri");
            drinks.Add("Gin & Tonic");
            drinks.Add("Flaming Moe");
            drinks.Add("Flaming Homer");
            drinks.Add("Mimosa");
            drinks.Add("Baileys");
            drinks.Add("Irish Coffee");
            drinks.Add("Cosmopolitan");
            drinks.Add("Screwdriver");
            drinks.Add("Absinthe");
            drinks.Add("Pomegranate Martini");
            drinks.Add("Apple Martini");
            drinks.Add("Gin Martini");
            drinks.Add("Dry Martini");
            drinks.Add("Wet Martini");
            drinks.Add("Vesper Martini");
            drinks.Add("Apple Cider Martini");
            drinks.Add("Blackberry Martini");
            drinks.Add("Vodka Martini, shaken, not stirred");
            drinks.Add("Vodka Martini, stirred, not shaken");
            drinks.Add("Vodka Martini, shaken AND stirred");
            drinks.Add("Vodka Martini, shaken XOR stirred");
            drinks.Add("Bronx");
            drinks.Add("Gibson");
            drinks.Add("Carlsberg");
            drinks.Add("Heineken");
            drinks.Add("Pabst Blue Ribbon");
            drinks.Add("Noilly Prat");
            drinks.Add("Nellie Prat");
            drinks.Add("Noilly Bly");
            drinks.Add("Pan-Eurasian Gargle Blaster");
            drinks.Add("Glingue");
            drinks.Add("Victory Gin");
            drinks.Add("Piso Mojado");
            drinks.Add("May Queen");
            drinks.Add("Moloko Plus");
            drinks.Add("Slurm");
            drinks.Add("Limonana");
            drinks.Add("Panther Pilsner Beer");
            drinks.Add("Sani-Cola");
            drinks.Add("Whisky");
            drinks.Add("Whiskey");
            drinks.Add("Suntory Whisky");
            drinks.Add("Glenfiddich");
            drinks.Add("Chivas Regal");
            drinks.Add("anCnoc");
            drinks.Add("Clontarf 1014");

            suggestions = drinks.Where(r => r.ToLower().Contains(term.ToLower())).ToList();
          }

          return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        Random rng = new Random();

        // GET: Social
        public ActionResult Index()
        {
            var events = db.SocialEvents.Where(x => x.UserName == User.Identity.Name && x.Enabled).ToList();
            ViewBag.Active = events.Count > 0;
            ViewBag.Percent = rng.Next(5, 21).ToString();
            return View(events);
        }

        // GET: Social/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            return View(socialEvent);
        }

        // GET: Social/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Social/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Title,Date,Attending,Contribution")] SocialEvent socialEvent)
        {
            if (ModelState.IsValid)
            {
                db.SocialEvents.Add(socialEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialEvent);
        }

        // GET: Social/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            if (socialEvent.Title.Contains("potluck"))
            {
                ViewBag.Suggestions = "PotluckSuggestions";
            }
            else if(socialEvent.Title.Contains("Onboarding"))
            {
                ViewBag.Suggestions = "OnboardingSuggestions";
            }
            else if (socialEvent.Title.Contains("club"))
            {
              ViewBag.Suggestions = "ClubSuggestions";
            }
            return View(socialEvent);
        }

        // POST: Social/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Title,Date,Attending,Enabled,Contribution")] SocialEvent socialEvent)
        {
            if (ModelState.IsValid)
            {
                socialEvent.Attending = true;
                socialEvent.Contribution = socialEvent.Contribution ?? "";
                db.Entry(socialEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialEvent);
        }

        // GET: Social/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            return View(socialEvent);
        }

        // POST: Social/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            db.SocialEvents.Remove(socialEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
