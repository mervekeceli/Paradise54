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
    public class DrinkManager : IDrinkService
    {
        IDrinkDal _drinkDal;
        public DrinkManager(IDrinkDal drink)
        {
            _drinkDal = drink;
        }
        public Drink GetById(int id)
        {
            return _drinkDal.GetById(id);
        }

        public List<Drink> GetList()
        {
            return _drinkDal.GetListAll();
        }

        public void TAdd(Drink t)
        {
            _drinkDal.Insert(t);
        }

        public void TDelete(Drink t)
        {
            _drinkDal.Delete(t);
        }

        public void TUpdate(Drink t)
        {
            _drinkDal.Update(t);
        }
    }
}
