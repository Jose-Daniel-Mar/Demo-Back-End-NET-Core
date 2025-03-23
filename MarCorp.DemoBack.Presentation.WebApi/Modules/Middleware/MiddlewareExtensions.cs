using MarCorp.DemoBack.Presentation.WebApi.Modules.GlobalException;

namespace MarCorp.DemoBack.Presentation.WebApi.Modules.Middleware
{
    /// <summary>
    /// Extension methods for adding middleware to the application.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the global exception handler middleware to the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder with the middleware added.</returns>
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
