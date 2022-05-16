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
    public class CartManager : ICartService
    {
        ICartDal _cartDal;
        public CartManager(ICartDal cart)
        {
            _cartDal = cart;
        }
        public Cart GetById(int id)
        {
             return _cartDal.GetById(id);
        }

        public List<Cart> GetList()
        {
            return _cartDal.GetListAll();
        }
        public List<Cart> GetCartById(int id)
        {
            return _cartDal.GetListAll(x => x.Id == id);
        }
        public Cart GetCartByTablenum(int tablenum)
        {
            return _cartDal.GetListAll(x => x.TableId == tablenum).FirstOrDefault();
        }
     
        public void TAdd(Cart t)
        {
            _cartDal.Insert(t);
        }

        public void TDelete(Cart t)
        {
            _cartDal.Delete(t);
        }

        public void TUpdate(Cart t)
        {
            _cartDal.Update(t);
        }
    }
}
