﻿using Microsoft.AspNetCore.Http;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.DataAccess {
    public interface IRecipeDataAccess {
        IEnumerable<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        int CreateRecipe(Recipe recipe);
        Task<bool> SaveRawRecipe(IFormFile file);
    }
}
