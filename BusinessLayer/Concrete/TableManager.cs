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
    public class TableManager : ITableService
    {
        ITableDal _tableDal;

        public TableManager(ITableDal table)
        {
            _tableDal = table;
        }
        public Table GetById(int id)
        {
            return _tableDal.GetById(id);
        }

        public List<Table> GetList()
        {
            return _tableDal.GetListAll();
        }

        public void TAdd(Table t)
        {
            _tableDal.Insert(t);
        }

        public void TDelete(Table t)
        {
            _tableDal.Delete(t);
        }

        public void TUpdate(Table t)
        {
            _tableDal.Update(t);
        }
    }
}
