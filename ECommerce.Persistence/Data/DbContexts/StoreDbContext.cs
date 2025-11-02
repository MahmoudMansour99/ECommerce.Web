using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Persistence.Data.Configrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

        #region Db Sets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductsBrands { get; set; }
        public DbSet<ProductType> ProductsTypes { get; set; }
        #endregion
    }
}
