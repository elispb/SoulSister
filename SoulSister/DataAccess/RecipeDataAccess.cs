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
        private List<Recipe> Recipes { get; }
        private string datafile { get; }

        public RecipeDataAccess() {

            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            datafile = Path.Combine(currentDirectory, @"Data\Recipes.json");

            if (File.Exists(datafile)) {
                string text = File.ReadAllText(datafile);
                this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(text);
            }
        }

        public Recipe GetRecipe(int id) {
            return this.Recipes.FirstOrDefault(recipe => recipe.ID == id);
        }

        public IEnumerable<Recipe> GetRecipes() {
            return this.Recipes;
        }

        public int CreateRecipe(Recipe recipe)
        {
            var last = this.Recipes.ToList().LastOrDefault();
            if(last != null)
            {
                recipe.ID = last.ID + 1;
                this.Recipes.Add(recipe);
                File.WriteAllText(datafile, JsonConvert.SerializeObject(this.Recipes));
            }
            return (int)last.ID + 1;
        }
    }
}
