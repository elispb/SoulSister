using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoulSisterSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SoulSisterSite.Controllers;

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

    public IActionResult Create()
    {
        return View();
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

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile recipe)
    {
        if(recipe == null)
        {
            return BadRequest("Please browse to select a file");
        }
         string[] mimeTypes = new[] { "image/jpeg", "image/png", "application/pdf", "image/tiff" };
        if (mimeTypes.Contains(recipe.ContentType))
        {
            using var client = new HttpClient();
            client.BaseAddress = BaseUri;
            var content = new MultipartFormDataContent();

            using var streamContent = new StreamContent(recipe.OpenReadStream());

            //streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(recipe.ContentType);
            content.Add(streamContent, "file", recipe.FileName);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            var response = await client.PostAsync("recipe/upload", content);

            return View("Success", new Success { Message = "File Uploaded" });
        }
        else
        {
            return BadRequest("File must be of type: png, jpeg, tiff, pdf");
        }
    }

    public IActionResult Success()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
