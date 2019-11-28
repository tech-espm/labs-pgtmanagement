using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PGTManagement.Gateway.PGTData;
using wExponencial.Service;

namespace PGTManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            AppSetting appSetting = Configuration.Get<AppSetting>();
            services.AddSingleton(instance => appSetting);

            services.AddTransient<IGroupClient, GroupClient>();
            services.AddTransient<IReviewClient, ReviewClient>();
            services.AddTransient<IStudentClient, StudentClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IGroupClient, GroupClient>();
            services.AddTransient<IStudentClient, StudentClient>();
            services.AddTransient<IReviewClient, ReviewClient>();

            string StorageConnectionString = Configuration["StorageConnectionString"];
            services.AddTransient(instance => new StorageService(StorageConnectionString));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
