using Microsoft.AspNetCore.Mvc;
using SoulSisterSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoulSisterSite.Controllers {
    public class RecipeController : Controller {
        public ActionResult Index() {
            IEnumerable<Recipe> recipes = null;

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:44370/");

                var responseTask = client.GetAsync("recipe");
                responseTask.Wait();
                
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Recipe>>();
                    readTask.Wait();

                    recipes = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    recipes = Enumerable.Empty<Recipe>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(recipes);
        }
        public ActionResult Recipe(int id) {
            Recipe recipe = null;

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:44370/");

                var responseTask = client.GetAsync($"recipe/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<Recipe>();
                    readTask.Wait();

                    recipe = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(recipe);
        }
    }
}
