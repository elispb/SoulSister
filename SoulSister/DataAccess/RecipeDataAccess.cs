using Newtonsoft.Json;
using SoulSister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.DataAccess {
    public class RecipeDataAccess {
        private IEnumerable<Recipe> Recipes { get; }
        static readonly string textFile = @"C:\Users\elisp\source\repos\SoulSister\SoulSister\Data\Recipes.txt";

        public RecipeDataAccess() {
            if (File.Exists(textFile)) {
                string text = File.ReadAllText(textFile);
                this.Recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(text);
            }
        }

        public Recipe GetRecipe() {
            return this.Recipes.FirstOrDefault();
        }

        public IEnumerable<Recipe> GetRecipes() {
            return this.Recipes;
        }
    }
}
