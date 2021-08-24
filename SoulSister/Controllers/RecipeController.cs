using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase { 

        [HttpGet]
        public IEnumerable<List<Recipe>> Get()
        {
            return this.NoContent();
        }
    }
}
