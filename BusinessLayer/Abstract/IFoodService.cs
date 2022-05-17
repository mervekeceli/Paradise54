using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFoodService : IGenericService<Food>
    {
        List<Food> GetFoodListWithCategory();

        List<Food> GetFoodListwithCategoryWithFilter(List<Food> foods,string catName);

        List<Food> GetSearchFoods(string searchItem);

        

    }
}
