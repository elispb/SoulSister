using System;
using System.Collections.Generic;

namespace SoulSister.Models {
    public class Recipe {

        public int? ID { get; set; }

        public string Name { get; set; }

        public int Serves { get; set; }

        public string Duration { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
        
        public IEnumerable<Allergen> Allergens { get; set; }

        public string Method { get; set; }
    }
}
