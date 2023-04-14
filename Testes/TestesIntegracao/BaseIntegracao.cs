using Bogus;
using Infraestrutura.Dados;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using Respawn.Graph;
using System;
using System.Threading.Tasks;
using WireMock.Server;

namespace TestesIntegracao
{
    public class BaseIntegracao
	{
		public IServiceScope Scope { get; private set; }
		public Faker Faker { get; private set; }
		//public HttpClientHelper HttpClientHelper { get; private set; }
		public WireMockServer wireMockServer
		{
			get
			{
				return AmbienteDeTestes.ServidorMock;
			}
		}

		private static Respawner _respawnerImFinanceDb;
		private string _conexaoDaAplicacao;

		protected Contexto _contexto;

		[OneTimeSetUp]
		public async Task OneTimeSetUp()
		{
			await SetUp(AmbienteDeTestes.Fabrica);
		}

		private async Task SetUp(WebApplicationFactory<Program> fabrica)
		{
			var httpClient = fabrica.CreateClient();
			//HttpClientHelper = new HttpClientHelper(httpClient);

			Scope = fabrica.Services.CreateScope();
			Faker = new Faker("pt_BR");

			_contexto = Scope.ServiceProvider.GetRequiredService<Contexto>();

			var configuration = AmbienteDeTestes.Fabrica.Services.GetService<IConfiguration>();

			_conexaoDaAplicacao = configuration.GetConnectionString("DefaultConnection");

			_respawnerImFinanceDb =
					await Respawner.CreateAsync(_conexaoDaAplicacao, new RespawnerOptions()
					{
						TablesToIgnore = new Table[]
							{
								"__EFMigrationsHistory",
							}
					});
		}

		public async Task<BaseIntegracao> CustomSetUp(Action<IServiceCollection> configureServices)
		{
			var factory = await new AmbienteDeTestes().CreateFactory(configureServices);
			await SetUp(factory);
			return this;
		}

		public async Task ResetDatabase()
		{
			await _respawnerImFinanceDb.ResetAsync(_conexaoDaAplicacao);
		}
	}
}
