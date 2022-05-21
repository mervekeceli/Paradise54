using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCartItemRepository:GenericRepository<CartItem>,ICartItemDal
    {
        public List<CartItem> GetListwithFoodCartIncludeFilter(int tableNum)
        {
            using (var c = new Context())
            {
                return c.CartItems.Include(x => x.Cart).Include(x => x.Food)
                    .Where(x => x.Cart.TableId == tableNum && x.Cart.Active == true && x.Active == true).ToList();
            }
        }
        public List<CartItem> ListwithFoodCartIdIncludeFilterCartItems(int cartId)
        {
            using (var c = new Context())
            {
                return c.CartItems.Include(x => x.Food)
                    .Where(x => x.CartId == cartId && x.Active == true).ToList();

            }
        }
        public CartItem CartItemFoodCartIncludeFilterCartItems(int cartId, int foodId)
        {
            using (var c = new Context())
            {
                return c.CartItems.Where(x => x.CartId == cartId && x.FoodId == foodId && x.Active == true).FirstOrDefault();

            }
        }
    }
}
