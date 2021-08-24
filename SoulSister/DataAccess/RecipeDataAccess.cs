using Newtonsoft.Json;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.DataAccess {
    public class RecipeDataAccess: IRecipeDataAccess {
        private IEnumerable<Recipe> Recipes { get; }
        static readonly string textFile = @"C:\Users\elisp\source\repos\SoulSister\SoulSister\Data\Recipes.json";

        public RecipeDataAccess() {
            if (File.Exists(textFile)) {
                string text = File.ReadAllText(textFile);
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
