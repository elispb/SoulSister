using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoulSisterSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace SoulSisterSite.Controllers {
    public class RecipeController : Controller {

        public RecipeController() {
        }

        public IActionResult Index() {
            IEnumerable<Recipe> recipes = null;

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://reciperecommenderapi.azurewebsites.net/");

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
        public IActionResult View(int id) {
            Recipe recipe = null;

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://reciperecommenderapi.azurewebsites.net/");

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

        public IActionResult Create() {
            return View(new Recipe());
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe) {
            return View(recipe);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
