using AutoMapper;
using MarCorp.DemoBack.Support.Mapper;

namespace MarCorp.DemoBack.Services.WebApi.Modules
{
    /// <summary>
    /// Extension methods for adding AutoMapper to the IServiceCollection.
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Adds AutoMapper services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingsProfile));

            return services;
        }
    }
}
