using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoulSister.DataAccess;
using SoulSister.Models;
using SoulSister.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoulSister.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    IRecipeDataAccess _dataAccess;
    IRecipeReadingService _recipeReadingService;

    public RecipeController(IRecipeDataAccess dataAccess, IRecipeReadingService recipeReadingService)
    {
        _dataAccess = dataAccess;
        _recipeReadingService = recipeReadingService;

    }

    [HttpGet]
    public IActionResult Get()
    {
        return this.Ok(this._dataAccess.GetRecipes());
    }

    [HttpGet]
    [Route("{recipeId}")]
    public IActionResult Get(int recipeId)
    {
        var result = this._dataAccess.GetRecipe(recipeId);

        return result != null
            ? this.Ok(result)
            : this.NotFound($"No Recipe found with id {recipeId}");
    }

    [HttpPost]
    public IActionResult Create([FromBody] JsonElement recipe)
    {
        var v = JsonConvert.DeserializeObject<Recipe>(recipe.ToString());
        return Ok(this._dataAccess.CreateRecipe(v));
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload(IFormFile recipeFile)
    {        
        return Ok(_recipeReadingService.RecipeImageToRecipe(recipeFile));
    }
}