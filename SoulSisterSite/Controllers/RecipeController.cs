using Microsoft.AspNetCore.Mvc;
using SoulSisterSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSisterSite.Controllers {
    public class RecipeController : Controller {
        public ActionResult Index() {
            //TODO get all recipes from api
            return View(new List<Recipe>() { new Recipe() { Name = "Test Recipe" }, new Recipe() { Name = "Test Recipe 2" } });
        }
        public ActionResult Recipe(int id) {
            ViewBag.RecipeId = id;

            //TODO query api for recipe of given ID
            return View(new Recipe() { Name = "Test Recipe" });
        }
    }
}
