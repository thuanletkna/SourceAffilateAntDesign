using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffilateSource.Data.Extensions;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Data.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AffilateSource.App
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICategoriesServices, CategoriesServices>();
            //services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddScoped<IPostServices, PostServices>();
            services.AddConfigSourceAffilateData(Configuration);
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
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("swagger");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Knowledge Space API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "Contact",
                  pattern: "/gioi-thieu",
                 new { controller = "Contact", action = "Index" });
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "/admin",
                   new { controller = "Admin", action = "Index" });
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "/create-product",
                   new { controller = "Admin", action = "CreateProduct" });
                endpoints.MapControllerRoute(
                 name: "Product",
                  pattern: "{SeoAlias}-{id}",
                 new { controller = "Product", action = "GetAllProductPaging" });
                endpoints.MapControllerRoute(
                 name: "Product",
                  pattern: "/sanpham/{SeoAlias}-{id}",
                 new { controller = "Product", action = "GetAllProductsPaging" });
                endpoints.MapControllerRoute(
                 name: "Product",
                  pattern: "/product/{SeoAlias}-{id}",
                 new { controller = "Product", action = "GetProductById" });

                endpoints.MapControllerRoute(
                 name: "Admin",
                 pattern: "/admin/create-post",
                 new { controller = "Admin", action = "CreatePost" });
                endpoints.MapControllerRoute(
                  name: "Admin",
                  pattern: "/create-category",
                  new { controller = "Admin", action = "CreateCategory" });

                endpoints.MapControllerRoute(
                 name: "Post",
                  pattern: "/blog/{SeoAlias}-{id}",
                 new { controller = "Post", action = "GetAllPostPaging" });
                endpoints.MapControllerRoute(
                 name: "Post",
                  pattern: "/details/{SeoAlias}-{id}",
                 new { controller = "Post", action = "GetPostById" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
