using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int FoodId { get; set; }

        public Food Food  { get; set; }

        public int DrinkId { get; set; }

        public Drink Drink { get; set; }
    }
}
