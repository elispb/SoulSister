using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoulSister.DataAccess;
using SoulSister.Models;
using System.Text.Json;

namespace SoulSister.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase {
    IRecipeDataAccess dataAccess;

    public RecipeController(IRecipeDataAccess dataAccess) {
        this.dataAccess = dataAccess;
    }

    [HttpGet]
    public IActionResult Get() {
        return  this.Ok(this.dataAccess.GetRecipes());
    }

    [HttpGet]
    [Route("{recipeId}")]
    public IActionResult Get(int recipeId)
    {
        var result = this.dataAccess.GetRecipe(recipeId);

        return result != null
            ? this.Ok(result)
            : this.NotFound($"No Recipe found with id {recipeId}");
    }

    [HttpPost]
    public IActionResult Create([FromBody] JsonElement recipe)
    {
        var v = JsonConvert.DeserializeObject<Recipe>(recipe.ToString());
        return Ok(this.dataAccess.CreateRecipe(v));
    }
}
