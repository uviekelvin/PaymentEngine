using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentEngine.Api.Filters;
using PaymentEngine.Infrastructure.Factory;
using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PaymentEngine.Api.StartUpHelperClass;

namespace PaymentEngine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(this.Configuration);
            services.AddApplicationDependencies(this.Configuration);
            services.AddSwagger();

            //services.AddTransient<PaymentGatewayFactory>(serviceProvider => key =>
            //{
            //    if (key <= 20m)
            //        return serviceProvider.GetService<CheapPaymentGateway>();

            //    else if (key > 20 && key <= 500)
            //        return serviceProvider.GetService<ExpensivePaymentGateway>();

            //    else return serviceProvider.GetService<PremiumPaymentService>();

            //});
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ModelValidationFilter));
                opt.Filters.Add(typeof(GlobalErrorHandler));

            })
         .ConfigureApiBehaviorOptions(options =>
       {
           options.SuppressModelStateInvalidFilter = true;
       });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<LoggerMiddlewareFilter>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "PaymentEngine API");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
