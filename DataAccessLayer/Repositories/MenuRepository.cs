using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MenuRepository : IMenuDal
    {
        Context c = new Context();
        public void Delete(Menu t)
        {
            throw new NotImplementedException();
        }

        public Menu GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Menu> GetListAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Menu t)
        {
            throw new NotImplementedException();
        }

        public void Update(Menu t)
        {
            throw new NotImplementedException();
        }
    }
}
