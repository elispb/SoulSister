using System;
using System.Collections.Generic;

namespace SoulSister
{
    public class Recipe
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public List<string> Ingredients {get; set;}

        public string Method { get; set; }
    }
}
