﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public IEnumerable<FoodIngredients> FoodIngredients { get; set; }
    }
}
