using EntityLinqPart2.Data;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ZXing;

namespace EntityLinqPart2.Aula4
{
    public class Exercicio1
    {
        private const string Imagens = "Imagens";

        public static void GerarQRCode()
        {
            var barcodWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 100,
                    Height = 100
                }
            };

            if (!Directory.Exists(Imagens)) Directory.CreateDirectory(Imagens);

            using (var contexto = new AluraTunesEntities1())
            {
                var queryFaixas =
                    from f in contexto.Faixas
                    select f;

                var listaFaixas = queryFaixas.ToList();

                var stopwatch = Stopwatch.StartNew();

                var queryCodigos =
                    listaFaixas
                    .AsParallel()
                    .Select(f => new
                    {
                        Arquivo = $@"{Imagens}\{f.FaixaId}.jpg",
                        Imagem = barcodWriter.Write($"aluratunes.com/faixa/{f.FaixaId}")
                    });

                var contagem = queryCodigos.Count();

                stopwatch.Stop();

                Console.WriteLine($"Códigos gerados: {contagem} em {stopwatch.ElapsedMilliseconds / 1000.0} segundos.");

                stopwatch = Stopwatch.StartNew();

                queryCodigos.ForAll(item => item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg));

                stopwatch.Stop();

                Console.WriteLine($"Códigos salvos em arquivos: {contagem} em {stopwatch.ElapsedMilliseconds / 1000.0} segundos."); //65,7
            }
        }
    }
}
