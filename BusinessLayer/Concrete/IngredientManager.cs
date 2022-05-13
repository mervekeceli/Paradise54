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
    public class IngredientManager : IIngredientService
    {
        IIngredientDal _ingredientDal;

        public IngredientManager(IIngredientDal ingredient)
        {
            _ingredientDal = ingredient;
        }
        public Ingredient GetById(int id)
        {
            return _ingredientDal.GetById(id);
        }

        public List<Ingredient> GetList()
        {
            return _ingredientDal.GetListAll();
        }

        public void TAdd(Ingredient t)
        {
            _ingredientDal.Insert(t);
        }

        public void TDelete(Ingredient t)
        {
            _ingredientDal.Delete(t);
        }

        public void TUpdate(Ingredient t)
        {
            _ingredientDal.Update(t);
        }
    }
}
