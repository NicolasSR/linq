using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula5
{
    public class Exercicio1
    {
        public static void AcessandoStoreProcedure()
        {
            using (var contexto = new AluraTunesEntities1())
            {
                const int clienteId = 1;
                var query =
                    from sp in contexto.ps_Vendas_Por_Cliente(clienteId)
                    group sp by new { sp.DataNotaFiscal.Year, sp.DataNotaFiscal.Month }
                    into agrupado
                    orderby agrupado.Key.Year, agrupado.Key.Month
                    select new
                    {
                        Ano = agrupado.Key.Year,
                        Mes = agrupado.Key.Month,
                        Total = agrupado.Sum(a => a.Total)
                    };

                foreach (var item in query)
                {
                    Console.WriteLine($"{item.Ano}\t{item.Mes}\t{item.Total}");
                }
            }
        }
    }
}
