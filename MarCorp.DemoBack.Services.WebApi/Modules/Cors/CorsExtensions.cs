namespace MarCorp.DemoBack.Services.WebApi.Modules.Cors
{
    /// <summary>
    /// Extension methods for configuring CORS in the application.
    /// </summary>
    public static class CorsExtensions
    {
        /// <summary>
        /// Adds CORS policy to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the CORS policy to.</param>
        /// <param name="configuration">The configuration to use for the CORS policy.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddFCors(this IServiceCollection services, IConfiguration configuration)
        {
            string myPolicy = "policyApiMarCorp";

            services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.WithOrigins(configuration["Config:OriginCors"])
                .AllowAnyHeader()
                .AllowAnyMethod()));

            services.AddMvc();

            return services;
        }
    }
}
