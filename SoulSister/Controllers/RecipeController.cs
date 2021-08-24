using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoulSister.DataAccess;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.Controllers {
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
        public IActionResult Get(int recipeId) {
            var result = this.dataAccess.GetRecipe(recipeId);

            return result != null
                ? this.Ok(result)
                : this.NotFound($"No Recipe found with id {recipeId}");
        }
    }
}
