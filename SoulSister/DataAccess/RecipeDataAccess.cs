using Newtonsoft.Json;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SoulSister.DataAccess {
    public class RecipeDataAccess: IRecipeDataAccess {
        private IEnumerable<Recipe> Recipes { get; }

        public RecipeDataAccess() {

            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string datafile = Path.Combine(currentDirectory, @"Data\Recipes.json");

            if (File.Exists(datafile)) {
                string text = File.ReadAllText(datafile);
                this.Recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(text);
            }
        }

        public Recipe GetRecipe(int id) {
            return this.Recipes.FirstOrDefault(recipe => recipe.ID == id);
        }

        public IEnumerable<Recipe> GetRecipes() {
            return this.Recipes;
        }
    }
}
