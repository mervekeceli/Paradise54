using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfFoodRepository : GenericRepository<Food>, IFoodDal
    {
        public List<Food> GetListwithCategory()
        {
            using (var c = new Context())
            {
                return c.Foods.Include(x => x.Category).ToList();
            }
        }

        public List<Food> GetListwithCategoryWithFilter(List<Food> foods, string catName)
        {
            return foods.Where(x => x.Category.Name == catName).ToList();
            
        }
        public List<Food> SearchFoods(string searchItem)
        {
            using(var c=new Context())
            {
                return c.Foods.Include(x=>x.Category).Where(x => x.Name.ToLower().Contains(searchItem.ToLower())).ToList();
            }
        }
        public List<Food> GetRelatedFoods(int foodId)
        {
            using (var c=new Context())
            {
                Food f=c.Foods.Include(x => x.Category).Where(x => x.Id == foodId).First();

                return c.Foods.Include(x => x.Category).Where(x => x.Category.Id==f.Category.Id).Take(3).ToList();
            }
        }

    }
}
