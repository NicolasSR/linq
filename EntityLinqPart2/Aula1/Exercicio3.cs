using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula1
{
    public class Exercicio3
    {
        public static void ProdutosMaisVendidos()
        {
            using (var contexto = new AluraTunesEntities1())
            {
                var queryFaixa =
                    from f in contexto.Faixas
                    where f.ItemNotaFiscals.Count() > 0
                    let Total = f.ItemNotaFiscals.Sum(i => i.Quantidade * i.PrecoUnitario)
                    orderby Total descending
                    select new
                    {
                        f.FaixaId,
                        f.Nome,
                        Total
                    };

                var produtoMaisVendido = queryFaixa.First();
                Console.WriteLine($"{produtoMaisVendido.FaixaId} {produtoMaisVendido.Nome} {produtoMaisVendido.Total}");

                var query =
                    from inf in contexto.ItemNotaFiscals
                    where inf.FaixaId == produtoMaisVendido.FaixaId
                    select new
                    {
                        Cliente = inf.NotaFiscal.Cliente.PrimeiroNome + " " + inf.NotaFiscal.Cliente.Sobrenome
                    };

                foreach (var inf in query)
                {
                    Console.WriteLine($"{inf.Cliente}");
                }
            }
        }
    }
}
