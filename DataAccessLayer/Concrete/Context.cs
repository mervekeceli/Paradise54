using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=MSI;database=ParadiseYeni54Db;integrated security=true;");
            //optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=PParadise54Db;integrated security=true;");
        }
        public DbSet<Food> Foods { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
