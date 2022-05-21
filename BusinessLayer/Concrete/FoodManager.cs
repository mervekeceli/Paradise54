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
    public class FoodManager : IFoodService
    {
        IFoodDal _foodDal;

        public FoodManager(IFoodDal food)
        {
            _foodDal = food;
        }
        public Food GetById(int id)
        {
            return _foodDal.GetById(id);
        }
        public List<Food> GetFoodById(int id)
        {
            return _foodDal.GetListAll(x => x.Id == id);
        }
        public List<Food> GetFoodListWithCategory()
        {
            return _foodDal.GetListwithCategory();
        }

        public List<Food> GetList()
        {
            return _foodDal.GetListAll();
        }

        public void TAdd(Food t)
        {
            _foodDal.Insert(t);
        }

        public void TDelete(Food t)
        {
            _foodDal.Delete(t);
        }

        public void TUpdate(Food t)
        {
            _foodDal.Update(t);
        }
        public List<Food> GetFoodListwithCategoryWithFilter(List<Food> foods, string catName)
        {
            return _foodDal.GetListwithCategoryWithFilter(foods,catName);
        }

        public List<Food> GetSearchFoods(string searchItem)
        {
            return _foodDal.SearchFoods(searchItem);
        }

        public bool TExistss(int id)
        {
            return _foodDal.TExists(x => x.Id == id);
        }
        public List<Food> GetListRelatedFoods(int foodId)
        {
            return _foodDal.GetRelatedFoods(foodId);
        }

        
    }
}
