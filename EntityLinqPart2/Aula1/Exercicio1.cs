using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula1
{
    public class Exercicio1
    {
        public static void RelatorioPaginado()
        {
            using (var contexto = new AluraTunesEntities1())
            {
                const int TAMANHO_PAGINA = 10;

                var numeroNotasFiscais = contexto.NotaFiscals.Count();
                var numeroPaginas = Math.Ceiling((decimal)numeroNotasFiscais / TAMANHO_PAGINA);

                for (int p = 1; p <= numeroPaginas; p++)
                {

                    ImprimirRelatorio(contexto, TAMANHO_PAGINA, p);
                }
            }
        }

        private static void ImprimirRelatorio(AluraTunesEntities1 contexto, int TAMANHO_PAGINA, int numeroPaginas)
        {
            var query =
                from nf in contexto.NotaFiscals
                orderby nf.NotaFiscalId
                select new
                {
                    Id = nf.NotaFiscalId,
                    Data = nf.DataNotaFiscal,
                    Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                    Total = nf.Total
                };

            var numeroDePulos = (numeroPaginas - 1) * TAMANHO_PAGINA;
            query = query.Skip(numeroDePulos);
            query = query.Take(TAMANHO_PAGINA);

            Console.WriteLine($"\n Número da página {numeroPaginas}");
            foreach (var nf in query)
            {
                Console.WriteLine($"{nf.Id}\t{nf.Data}\t{nf.Cliente}\t{nf.Total}");
            }
        }
    }
}
