﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public string Note { get; set; }
        public bool Active { get; set; }
    }
}
