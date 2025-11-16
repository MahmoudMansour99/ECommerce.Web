using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.DataSeed;
using ECommerce.Persistence.Data.DbContexts;
using ECommerce.Persistence.Repositories;
using ECommerce.Services;
using ECommerce.Services.MappingProfiles;
using ECommerce.Services_Abstractions;
using ECommerce.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Service to the Container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddAutoMapper(X => X.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(typeof(ServiceAssemplyReference).Assembly);
            //builder.Services.AddAutoMapper(X => X.LicenseKey = "", typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<ProductPictureUrlResolver>();
            #endregion

            var app = builder.Build();

            #region DataSeed - Apply Migrations
            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();
            #endregion

            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
