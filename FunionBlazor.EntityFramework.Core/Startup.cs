using Furion;
using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FunionBlazor.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>($"{DbProvider.MySql}@8.0.22");
            }, "FunionBlazor.Database.Migrations");
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>(providerName: default, optionBuilder: (services, opt) => // 如果是 v3.7.11 之前，使用 opt =>
                {
                    opt.UseMySQL();    
                });
            });
        }
    }
}