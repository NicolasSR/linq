using EntityLinqPart2.Data;
using System;
using System.Linq;

namespace EntityLinqPart2.Aula3
{
    public class Exercicio1
    {
        public static void ConsultaImediataEAdiada()
        {
            using (var contexto = new AluraTunesEntities1())
            {
                var mes = 1;

                while (mes <= 12)
                {
                    Console.WriteLine($"Mês: {mes}");

                    var query =
                        (from f in contexto.Funcionarios
                        where f.DataNascimento.Value.Month == mes
                        orderby f.DataNascimento.Value.Month, f.DataNascimento.Value.Day
                        select f).ToList();

                    mes++;

                    foreach (var f in query)
                    {
                        Console.WriteLine($"{f.DataNascimento.Value.ToString("dd/MM")} \t {f.PrimeiroNome} {f.Sobrenome}");
                    }
                }
            }
        }
    }
}
