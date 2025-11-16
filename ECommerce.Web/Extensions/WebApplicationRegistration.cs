using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DbContextService = Scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                await DbContextService.Database.MigrateAsync();

            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DataIntializerService = Scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await DataIntializerService.IntializeAsync();
            return app;
        }
    }
}
