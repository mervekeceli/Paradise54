﻿using BusinessLayer.Abstract;
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
    }
}