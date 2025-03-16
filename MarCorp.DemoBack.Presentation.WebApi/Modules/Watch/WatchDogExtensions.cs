using WatchDog;
using WatchDog.src.Enums;

namespace MarCorp.DemoBack.Services.WebApi.Modules.Watch
{
    /// <summary>
    /// Extension methods for setting up WatchDog services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class WatchDogExtensions
    {
        /// <summary>
        /// Adds WatchDog services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to configure the services.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(opt =>
            {
                opt.SetExternalDbConnString = configuration.GetConnectionString("NorthwindConnection");
                opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;                
            });
            return services;
        }
    }
}
