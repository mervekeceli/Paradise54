using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MenuManager : IMenuService
    {
        IMenuDal _menuDal;
        public MenuManager(IMenuDal menu)
        {
            _menuDal = menu;
        }
        public Menu GetById(int id)
        {
            return _menuDal.GetById(id);
        }

        public List<Menu> GetList()
        {
            return _menuDal.GetListAll();
        }

        public void TAdd(Menu t)
        {
            _menuDal.Insert(t);
        }

        public void TDelete(Menu t)
        {
            _menuDal.Delete(t);
        }

        public void TUpdate(Menu t)
        {
            _menuDal.Update(t);
        }
    }
}
