using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CartItemManager : ICartItemService
    {
        ICartItemDal _cartItemDal;
        public CartItemManager(ICartItemDal cartitem)
        {
            _cartItemDal = cartitem;
        }
        public CartItem GetById(int id)
        {
             return _cartItemDal.GetById(id);
        }

        public List<CartItem> GetList()
        {
            return _cartItemDal.GetListAll();
        }
        public List<CartItem> GetCartItemById(int id)
        {
            return _cartItemDal.GetListAll(x => x.Id == id);
        }
        
     
        public void TAdd(CartItem t)
        {
            _cartItemDal.Insert(t);
        }

        public void TDelete(CartItem t)
        {
            _cartItemDal.Delete(t);
        }

        public void TUpdate(CartItem t)
        {
            _cartItemDal.Update(t);
        }

        public List<CartItem> GetCartItemListwithFoodCartIncludeFilter(int tableNum)
        {
            return _cartItemDal.GetListwithFoodCartIncludeFilter(tableNum);
        }

        public List<CartItem> GetListwithFoodCartIdIncludeFilterCartItems(int cartId)
        {
            return _cartItemDal.ListwithFoodCartIdIncludeFilterCartItems(cartId);
        }

        public bool TExistss(int id)
        {
            return _cartItemDal.TExists(x => x.Id == id);
        }
        public CartItem GetCartItemFoodCartIncludeFilterCartItems(int tableNum, int foodId)
        {
            return _cartItemDal.CartItemFoodCartIncludeFilterCartItems(tableNum, foodId);
        }
    }
}
