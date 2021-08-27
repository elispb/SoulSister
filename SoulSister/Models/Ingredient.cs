using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulSister.Models {
    public class Ingredient {
        public string? Quantity { get; set; }
        public Unit? Unit { get; set; }
        public string? Descriptor { get; set; }
        public string Name { get; set; }
    }
}
