using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PaymentEngine.Infrastructure.DataAccess;
using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Repositories;
using PaymentEngine.Infrastructure.Repositories.Interfaces;
using PaymentEngine.Infrastructure.Services;
using PaymentEngine.Infrastructure.Services.Interfaces;
using PaymentEngine.Infrastructure.Strategy;
using PaymentEngine.Infrastructure.Strategy.Interfaces;
namespace PaymentEngine.Api
{
    public static class StartUpHelperClass
    {

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentEngineContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PaymentDb"));
            });
        }
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Startup));
            services.Configure<AppSettingsDtos>(configuration.GetSection("AppSettings"));
            services.AddScoped(sp => sp.GetService<IOptionsSnapshot<AppSettingsDtos>>().Value);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentStrategy, PaymentStrategy>();
            services.AddScoped<IPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IPaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPaymentGateway, PremiumPaymentService>();
            // services.AddTransient<CheapPaymentGateway>();
            // services.AddTransient<ExpensivePaymentGateway>();
            // services.AddTransient<PremiumPaymentService>();
            // services.AddScoped<IPaymentGatewayFactory>(provider =>
            //new PaymentGatewayFactory(provider));


        }
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentEngine Service API", Version = "v1", Description = "PaymentEngine Service API Documentation" });

            });
        }
    }
}
