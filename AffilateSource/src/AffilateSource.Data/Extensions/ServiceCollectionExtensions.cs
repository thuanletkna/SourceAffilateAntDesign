using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Data.Services.Repository;
using AffilateSource.Data.Services.ServicesConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AffilateSource.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigSourceAffilateData(this IServiceCollection services, IConfiguration Configuration)
        {
            //services
            services.AddDbContextPool<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPostServices, PostServices>();
            services.AddTransient<ICategoriesServices, CategoriesServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<ISequenceService, SequenceService>();
            services.AddTransient<IStorageService,FileStorageService>();
            services.AddTransient<IContactServices,ContactServices>();
            var sqlConnectionConfiguration = new SqlConnectionConfiguration(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(sqlConnectionConfiguration);
            return services;
        }
    }
}