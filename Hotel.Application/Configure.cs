using FluentValidation;
using HotelSevice.Application.Pipelines;
using MassTransit;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSevice.Application
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(Configure).Assembly;

            services.AddMediatR(assembly);

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            services.AddMassTransitHostedService();

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(RemoveCacheBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        }
    }

}
