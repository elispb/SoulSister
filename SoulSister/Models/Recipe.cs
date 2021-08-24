using System;
using System.Collections.Generic;

namespace SoulSister.Models {
    public class Recipe
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public List<Ingredient> Ingredients {get; set;}

        public string Method { get; set; }
    }
}
