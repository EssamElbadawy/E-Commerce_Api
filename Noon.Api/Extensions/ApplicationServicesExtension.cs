using Microsoft.AspNetCore.Mvc;
using Noon.Api.Errors;
using Noon.Core;
using Noon.Core.Repositories;
using Noon.Core.Services;
using Noon.Repository;
using Noon.Services;

namespace Noon.Api.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddAutoMapper(typeof(Program).Assembly);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(m => m.Value!.Errors.Count > 0).SelectMany(m => m.Value!.Errors)
                        .Select(e => e.ErrorMessage);
                    var response = new ApiValidationResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
