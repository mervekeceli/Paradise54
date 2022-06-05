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

        public List<CartItem> GetOrderListwithFoodCartIncludeFilter(int tableNum)
        {
            using (var c = new Context())
            {
                return c.CartItems.Include(x => x.Cart).Include(x => x.Food)
                    .Where(x => x.Cart.TableId == tableNum && x.Cart.Active == false && x.Active == true).ToList();
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
        public List<CartItem> GetAllOrdersCartItems()
        {
            using (var c = new Context())
            {
                return c.CartItems.Include(x => x.Cart).Where(x => x.Cart.Status == "SIPARIS" && x.Active == true).ToList();/*.GroupBy(x=>x.CartId).Select(x=>new { CartId=x.Key, TotalPrice = x.Sum(g=>g.Food.Price)}).ToList();//Group by yapilabilir*/

            }
        }
        public List<Orders> GetOrders(List<CartItem> deneme)
        {
            List<Orders> denemeList=new List<Orders>();
            var list= deneme.GroupBy(x => x.CartId).Select(x => new { CartId = x.Key, Price = x.Sum(x => x.Food.Price) }).ToList();
            for(int i=0;i<list.Count();i++)
            {
                Orders o = new Orders();
                o.CartId = list[i].CartId;
                o.Price = list[i].Price;
                denemeList.Add(o);
            }
            return denemeList;
            
        }
        public List<CartItem> GetDoneOrders()
        {
            using (var c = new Context())
            {
                return c.CartItems.Include(x => x.Cart).Include(x=>x.Food).Where(x => x.Cart.Status == "TAMAMLANDI").ToList(); 

            }
        }
    }
}
