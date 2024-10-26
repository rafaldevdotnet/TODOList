using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TODOList.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(mr => mr.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
