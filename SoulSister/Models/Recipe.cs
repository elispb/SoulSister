using System;
using System.Collections.Generic;

namespace SoulSister.Models {
    public class Recipe
    {
        public string Name { get; set; }

        public string Duration { get; set; }

        public IEnumerable<Ingredient> Ingredients {get; set;}

        public string Method { get; set; }
    }
}
