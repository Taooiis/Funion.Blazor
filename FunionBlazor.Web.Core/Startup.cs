using FunionBlazor.Core;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunionBlazor.Web.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsoleFormatter();
            services.AddControllers().AddInjectWithUnifyResult();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMasaBlazor();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                //hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });
            services.AddHostedService<CPZWorker>();
            services.AddHostedService<GDHWorker>();
            services.AddHostedService<MateWorker>();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseInject();

            app.UseEndpoints(endpoints =>
            {
                // 注册集线器
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/hubs/chathub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}