using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Application.UseCases.Users;
using MarCorp.DemoBack.Application.Validator;
using MarCorp.DemoBack.Persistence.Contexts;
using MarCorp.DemoBack.Persistence.Repositories;
using MarCorp.DemoBack.Support.Common;
using MarCorp.DemoBack.Support.Logging;
using MarCorp.DemoBack.Support.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarCorp.DemoBack.Application.Test
{
    [TestClass]
    public sealed class UsersApplicationTest
    {
        private static IConfiguration? _configuration;
        private static IServiceScopeFactory? _scopeFactory;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            
            var services = new ServiceCollection();

            services.AddSingleton(_configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddAutoMapper(typeof(MappingsProfile));
            services.AddTransient<UsersDTOValidator>();

            var serviceProvider = services.BuildServiceProvider();
            _scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task Authenticate_CuandoNoSeEnvianParametros_RetornaMensajeErrorValidacion()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUsersApplication>();

                // Datos de prueba
                var username = string.Empty;
                var password = string.Empty;
                var expected = "Errores de validación";

                // Actuar: intentar iniciar sesión
                var result = await context.AuthenticateAsync(username, password);

                // Afirmar: verificar que el inicio de sesión fue exitoso
                Assert.AreEqual(expected, result.Message);
            }
        }

        [TestMethod]
        public async Task Authenticate_CuandoSeEnvianParametrosCorrectos_RetornaMensajeExito()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUsersApplication>();

                // Datos de prueba
                var username = "danielmar";
                var password = "123456789";
                var expected = "Autenticación Exitosa!!!";

                // Actuar: intentar iniciar sesión
                var result = await context.AuthenticateAsync(username, password);

                // Afirmar: verificar que el inicio de sesión fue exitoso
                Assert.AreEqual(expected, result.Message);
            }
        }

        [TestMethod]
        public async Task Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornaMensajeUsuarioNOExiste()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUsersApplication>();

                // Datos de prueba
                var username = "otro_daniel";
                var password = "666666666";
                var expected = "Usuario no existe";

                // Actuar: intentar iniciar sesión
                var result = await context.AuthenticateAsync(username, password);

                // Afirmar: verificar que el inicio de sesión fue exitoso
                Assert.AreEqual(expected, result.Message);
            }
        }
    }
}
