using FluentValidation;
using HotelSevice.Application.Pipelines;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(Configure).Assembly;

            services.AddMediatR(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(RemoveCacheBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));
        }
    }

}
