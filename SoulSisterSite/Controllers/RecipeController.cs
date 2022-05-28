using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SoulSisterSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SoulSisterSite.Controllers {
    public class RecipeController : Controller {

        private Uri BaseUri { get; }

        public RecipeController(IConfiguration configuration) {
            BaseUri = new Uri(configuration.GetSection("ApiBaseUri").Value);
        }

        public IActionResult Index() {
            IEnumerable<Recipe> recipes = null;

            using (var client = new HttpClient()) {
                client.BaseAddress = BaseUri;

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
                client.BaseAddress = BaseUri;

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
        public IActionResult Create(string Recipe) {
            var canSerialise = JsonConvert.DeserializeObject<Recipe>(Recipe);

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;
                var content = new StringContent(Recipe, UnicodeEncoding.UTF8, "application/json");
                var responseTask = client.PostAsync($"recipe/", content);
                responseTask.Wait(); 

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();

                    canSerialise.ID = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View("View", canSerialise);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
