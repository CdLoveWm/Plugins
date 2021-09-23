using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugin.Demo.Hangfire.Context;
using Plugin.Demo.Hangfire.Extensions;
using Plugin.Demo.Hangfire.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Demo.Hangfire
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string connectStr;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectStr = Configuration.GetConnectionString("HangfireDemoConnection");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MySQL
            services.AddDbContext<DataContext>(option =>
            {
                option.UseMySql(connectStr, ServerVersion.AutoDetect(connectStr));
            });
            #endregion

            services.InitService();

            #region Hangfire
            services.AddHangfire(config => {
                config.UseStorage(new MySqlStorage(connectStr, new MySqlStorageOptions()
                {
                    TablesPrefix = "HF_"
                }));
            });
            services.AddHangfireServer();
            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            #region Hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions());
            RecurringJob.AddOrUpdate<ITestJob>(it => it.Excute(), Cron.Minutely, TimeZoneInfo.Local);
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
