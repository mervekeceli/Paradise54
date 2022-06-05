using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICartItemDal : IGenericDal<CartItem>
    {
        public List<CartItem> GetOrderListwithFoodCartIncludeFilter(int tableNum);

        public List<CartItem> GetListwithFoodCartIncludeFilter(int tableNum);

        public List<CartItem> ListwithFoodCartIdIncludeFilterCartItems(int cartId);

        public CartItem CartItemFoodCartIncludeFilterCartItems(int cartId, int foodId);

        public List<CartItem> GetAllOrdersCartItems();

        public List<Orders> GetOrders(List<CartItem> deneme);

        public List<CartItem> GetDoneOrders();
    }
}
