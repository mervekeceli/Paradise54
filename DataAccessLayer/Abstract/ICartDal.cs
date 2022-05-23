using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICartDal : IGenericDal<Cart>
    {
        public Cart GetListFilter(int tableNum);

        public Cart GetCartwithTableId(int tableId);
    }
}
