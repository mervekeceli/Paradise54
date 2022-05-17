using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCartRepository:GenericRepository<Cart>,ICartDal
    {
        public Cart GetListFilter(int tableNum)
        {
            using (var c = new Context())
            {
                return c.Carts.Where(x => x.TableId == tableNum && x.Active == true)
               .FirstOrDefault();
            }
        }
    }
}
