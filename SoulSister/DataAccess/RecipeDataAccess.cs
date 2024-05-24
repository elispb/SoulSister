using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SoulSister.DataAccess
{
    public class RecipeDataAccess : IRecipeDataAccess
    {
        private List<Recipe> Recipes { get; init; }
        private string Datafile { get; init; }
        private string CurrentDir { get; init; }

        public RecipeDataAccess()
        {

            CurrentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Datafile = Path.Combine(CurrentDir, @"Data\Recipes.json");

            if (File.Exists(Datafile))
            {
                string text = File.ReadAllText(Datafile);
                Recipes = JsonConvert.DeserializeObject<List<Recipe>>(text);
            }
        }

        public Recipe GetRecipe(int id)
        {
            return this.Recipes.FirstOrDefault(recipe => recipe.ID == id);
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return this.Recipes;
        }

        public int CreateRecipe(Recipe recipe)
        {
            var last = this.Recipes.ToList().LastOrDefault();
            if (last != null)
            {
                recipe.ID = last.ID + 1;
                this.Recipes.Add(recipe);
                File.WriteAllText(Datafile, JsonConvert.SerializeObject(this.Recipes));
            }
            return (int)last.ID + 1;
        }

        public async Task<bool> SaveRawRecipe(IFormFile file)
        {
            var filePath = Path.Combine(CurrentDir, @"Data\Temp", file.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return true;
        }
    }
}
