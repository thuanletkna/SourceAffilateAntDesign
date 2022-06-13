using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AffilateSource.Client.Model;
using AffilateSource.Client.Services;
using AffilateSource.Client.Services.Interface;
using AffilateSource.Client.Services.Repository;
using AntDesign.ProLayout;
using Autofac;
using Autofac.Core.Activators.Reflection;
using Autofac.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AffilateSource.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));
            builder.Services.AddTelerikBlazor();
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            builder.Services.AddScoped<PostApiServices>();
            //builder.Services.AddScoped<IStatusService, StatusService>();
            builder.Services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<CustomUserFactory>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            await builder.Build().RunAsync();
        }
        private static void ConfigureContainer(ContainerBuilder builder)
        {
            // add any registrations here

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
        public class AllConstructorFinder : IConstructorFinder
        {
            private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
                new ConcurrentDictionary<Type, ConstructorInfo[]>();

            public ConstructorInfo[] FindConstructors(Type targetType)
            {
                var result = Cache.GetOrAdd(
                    targetType,
                    t => t.GetTypeInfo().DeclaredConstructors.ToArray());

                return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
            }
        }
    }
}