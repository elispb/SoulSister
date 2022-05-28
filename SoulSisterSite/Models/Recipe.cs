using System;
using System.Collections.Generic;

namespace SoulSisterSite.Models {
    public class Recipe {

        public Recipe() {
            this.Ingredients = new List<Ingredient>();
        }

        public int? ID { get; set; }

        public string Name { get; set; }

        public int Serves { get; set; }

        public string Duration { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        
        public List<Allergen> Allergens { get; set; }

        public string Method { get; set; }
    }
}
