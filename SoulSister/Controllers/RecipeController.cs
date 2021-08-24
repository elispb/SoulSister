using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        List<Recipe> cannedData;

        public RecipeController()
        {
            cannedData = new List<Recipe>() { new Recipe() {
                Name = "Cheese on Toast",
                Duration = new TimeSpan(0,10,0),
                Ingredients = new List<Ingredient>(){ new Ingredient(){Name = "Bread" }, new Ingredient(){Name = "Cheese"} },
                Method = "Grate Cheese\r\nLightly Toast bread\r\nPlace cheese on toast\r\nPlace cheese on toast under the grill for 5 mins until the cheese has melted"
            } };
        }

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return this.cannedData;
        }
    }
}
