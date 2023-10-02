using FluentAssertions;
using projeto_dotnet_testes.Helpers;

namespace TDD.HelpersTestes
{
    public class OperacoesTestes
    {
        [TestCase(1, 2, 3)]
        [TestCase(-5, 1, -4)]
        public void Adiciona_DeveSomarDoisNumeros_Corretamente(int num1, int num2, int resultadoEsperado)
        {
            var resultado = Operacoes.Adicionar(num1, num2);

            resultado.Should().Be(resultadoEsperado);
        }
    }
}
