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
    public class TableRepository : ITableDal
    {
        Context c = new Context();

        public void Delete(Table t)
        {
            c.Remove(t);
            c.SaveChanges();
        }

        public Table GetById(int id)
        {
            return c.Tables.Find(id);
        }

        public List<Table> GetListAll()
        {
            return c.Tables.ToList();
        }

        public void Insert(Table t)
        {
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(Table t)
        {
            c.Update(t);
            c.SaveChanges();
        }
    }
}
