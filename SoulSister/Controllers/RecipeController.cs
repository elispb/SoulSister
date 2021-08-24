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
        public IEnumerable<Recipe> Get() {
            return this.dataAccess.GetRecipes();
        }
    }
}
