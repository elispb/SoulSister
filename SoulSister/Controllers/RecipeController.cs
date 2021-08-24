using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoulSister.DataAccess;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase {
        IEnumerable<Recipe> cannedData;
        RecipeDataAccess dataAccess;

        public RecipeController() {
            this.dataAccess = new RecipeDataAccess();
        }

        [HttpGet]
        public IEnumerable<Recipe> Get() {
            return this.dataAccess.GetRecipes();
        }
    }
}
