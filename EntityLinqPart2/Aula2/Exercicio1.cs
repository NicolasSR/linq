using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula2
{
    public class Exercicio1
    {
        public static void AnaliseDeAfinidade()
        {
            const int faixaId = 1;
            using (var contexto = new AluraTunesEntities1())
            {

                Console.WriteLine("Quem Comprou...\n");
                var produtoProcurado = contexto.Faixas.Single(f => f.FaixaId == faixaId);
                Console.WriteLine($"{produtoProcurado.Nome}\n");

                Console.WriteLine("...também comprou...");

                var query =
                    from comprouItem in contexto.ItemNotaFiscals
                    join comprouTambem in contexto.ItemNotaFiscals
                    on comprouItem.NotaFiscalId equals comprouTambem.NotaFiscalId
                    where comprouItem.ItemNotaFiscalId != comprouTambem.ItemNotaFiscalId
                    && comprouItem.FaixaId == faixaId
                    select new { comprouItem, comprouTambem };

                foreach (var item in query)
                {
                    Console.WriteLine($"{item.comprouTambem.NotaFiscalId}\t{item.comprouTambem.Faixa.Nome}");
                }
            }
        }
    }
}
