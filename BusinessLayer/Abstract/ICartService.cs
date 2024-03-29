﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICartService : IGenericService<Cart>
    {
        public Cart GetCartListFilter(int tableNum);

        public Cart GetCartwithTableId(int tableId);
    }
}
