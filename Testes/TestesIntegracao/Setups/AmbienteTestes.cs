//using Microsoft.AspNetCore.Mvc.Testing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WireMock.Server;

//namespace TestesIntegracao.Setups
//{
//    [SetUpFixture]
//    public class AmbienteDeTestes
//    {
//        private static WebApplicationFactory<Program> Fabrica { get; set; }
//        public static WireMockServer ServidorMock { get; private set; }
//        private Contexto _contexto;

//        [OneTimeSetUp]
//        public async Task OneTimeSetup()
//        {
//            Fabrica = await CreateFactory();
//            ServidorMock = WireMockServer.Start(9876);
//        }

//        private async Task<WebApplicationFactory<Program>> CreateFactory(
//            Action<IServiceCollection> configureServices = null
//        )
//        {
//            var factory = new WebApplicationFactory<Program>()
//                .WithWebHostBuilder(builder =>
//                {
//                    builder
//                        .ConfigureAppConfiguration((_, config) =>
//                        {
//                            config.SetBasePath(Directory.GetCurrentDirectory());
//                            config.AddJsonFile("appsettings.Testing.json", true, true);
//                        })
//                        .UseContentRoot(Directory.GetCurrentDirectory())
//                        .UseEnvironment("Testing");

//                    if (configureServices != null)
//                    {
//                        builder.ConfigureServices(configureServices);
//                    }
//                });
//            using var scope = factory.Services.CreateScope();

//            _contexto = scope.ServiceProvider.GetService<Contexto>();

//            await _contexto.Database.MigrateAsync();
//            await AdicionaTabelasSemMigracao();

//            return factory;
//        }

//        [OneTimeTearDown]
//        public async Task OneTimeTearDown()
//        {
//            if (Fabrica != null)
//            {
//                await Fabrica.DisposeAsync();
//            }
//            if (ServidorMock != null)
//            {
//                ServidorMock.Stop();
//            }
//        }

//        private async Task AdicionaTabelasSemMigracao()
//        {
//            var scriptTabelas =
//                await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Setups", "CreateTables.sql"));

//            await _contexto.Database.ExecuteSqlRawAsync(scriptTabelas);
//        }
//    }
//}
