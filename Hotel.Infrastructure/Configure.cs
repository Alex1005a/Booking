using HotelSevice.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Infrastructure
{
    public static class Configure
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(ISearchPort), typeof(ElasticsearchAdapter));
        }
    }
}
