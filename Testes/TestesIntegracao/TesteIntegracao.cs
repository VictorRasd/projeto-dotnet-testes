using FluentAssertions;
using Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace TestesIntegracao
{
    public class TestIntegracao : BaseIntegracao
    {

        [Test]
        public void DeveExistirTabelaDeProduto()
        {
            bool existeTabelaDeProduto;

            var context = Scope.ServiceProvider.GetRequiredService<Contexto>();
            existeTabelaDeProduto =
                context.Produto.FromSqlRaw(@"SELECT 1 FROM sys.tables AS T WHERE T.Name = 'Produto'").SingleOrDefaultAsync() != null;

            existeTabelaDeProduto.Should().BeTrue();
        }
    }
}