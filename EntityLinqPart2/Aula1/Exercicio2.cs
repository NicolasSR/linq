using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula1
{
    public class Exercicio2
    {
        public static void SubConsulta()
        {
            using (var contexto = new AluraTunesEntities1())
            {
                var mediaNotasFicais = contexto.NotaFiscals.Average(n => n.Total);
                var query =
                    from nf in contexto.NotaFiscals
                    where nf.Total > mediaNotasFicais
                    orderby nf.Total descending
                    select new
                    {
                        Id = nf.NotaFiscalId,
                        Data = nf.DataNotaFiscal,
                        Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                        Total = nf.Total
                    };

                foreach (var nf in query)
                {
                    Console.WriteLine($"{nf.Id}\t{nf.Data}\t{nf.Cliente}\t{nf.Total}");
                }
                Console.WriteLine($"Media notas fiscais {mediaNotasFicais}");
            }
        }
    }
}
