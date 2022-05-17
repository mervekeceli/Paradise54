using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICartItemService : IGenericService<CartItem>
    {
        public List<CartItem> GetCartItemListwithFoodCartIncludeFilter(int tableNum);

        public List<CartItem> GetListwithFoodCartIdIncludeFilterCartItems(int cartId);
    }
}
