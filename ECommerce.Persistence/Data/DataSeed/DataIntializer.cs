using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.DataSeed
{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task IntializeAsync()
        {
            try
            {
                var HasProducts = await _dbContext.Products.AnyAsync();
                var HasBrands = await _dbContext.ProductsBrands.AnyAsync();
                var HasTypes = await _dbContext.ProductsTypes.AnyAsync();

                if (HasBrands && HasTypes && HasProducts) return;

                if (!HasBrands)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _dbContext.ProductsBrands);
                }
                if (!HasTypes)
                {
                    await SeedDataFromJsonAsync<ProductType, int>("types.json", _dbContext.ProductsTypes);
                }
                if (!HasProducts)
                {
                    await SeedDataFromJsonAsync<Product, int>("products.json", _dbContext.Products);
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seed is Failed {ex}");
            }
        }

        private async Task SeedDataFromJsonAsync<T, TKey>(string FileName, DbSet<T> dbSet) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\ECommerce.Persistence\Data\DataSeed\JSONFiles\" + FileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"Json File Not Found at Path: {FilePath}");

            try
            {
                using var dataStreams = File.OpenRead(FilePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStreams, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                {
                    await dbSet.AddRangeAsync(data);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading Json File: {ex}");
                return;
            }
        }
    }


}
