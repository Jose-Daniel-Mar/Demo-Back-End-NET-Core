using Microsoft.Extensions.DependencyInjection;
using MarCorp.DemoBack.Services.WebApi.Modules.Validator;
using MarCorp.DemoBack.Application.Validator;
using FluentValidation;
using MarCorp.DemoBack.Application.DTO;

namespace MarCorp.DemoBack.Services.WebApi.Modules.Validator
{
    /// <summary>
    /// Extension methods for adding validators to the service collection.
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Adds the UsersDTOValidator to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the validator to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UsersDTO>, UsersDTOValidator>();
            return services;
        }
    }
}
