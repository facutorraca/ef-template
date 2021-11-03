using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ef_template.Data;
using Microsoft.AspNetCore.Mvc;

namespace ef_template 
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<TemplateContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            
            // The AddDatabaseDeveloperPageExceptionFilter provides
            // helpful error information in the development environment.
            services.AddDatabaseDeveloperPageExceptionFilter();


            var mvc = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            if (Environment.IsDevelopment()) mvc.AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days.
                // You may want to change this for
                // production scenarios10,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
