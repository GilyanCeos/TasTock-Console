using TasTock.Models;
using System.Collections.Generic;
using System.Linq;

namespace TasTock.Repositories
{

    public class RelatorioRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Registrar(Relatorio relatorio)
        {
            _context.Relatorios.Add(relatorio);
            _context.SaveChanges();
        }

        public List<Relatorio> ListarPorPeriodo(DateTime inicio, DateTime fim)
        {
            using var context = new AppDbContext();
            return _context.Relatorios
                .Where(r => r.Data >= inicio && r.Data <= fim)
                .ToList();
        }
        public List<Relatorio> Listar()
        {
            using var context = new AppDbContext();
            return context.Relatorios.ToList();
        }
       public void ExportarRelatorios(List<Relatorio> relatorios, string periodo, DateTime dataInicio, DateTime dataFim)
        {
            if (relatorios == null || !relatorios.Any()) return;

            string dataFormatadaInicio = dataInicio.ToString("dd-MM-yyyy");
            string dataFormatadaFim = dataFim.ToString("dd-MM-yyyy");
            string nomeArquivo = $"relatorio_{dataFormatadaInicio}_a_{dataFormatadaFim}.csv";

            using var writer = new StreamWriter(nomeArquivo);
            writer.WriteLine("Data,Tipo,NomeItem,Quantidade,ValorTotal");

            foreach (var r in relatorios)
            {
                writer.WriteLine($"{r.Data:yyyy-MM-dd HH:mm},{r.Tipo},{r.NomeItem},{r.Quantidade},{r.ValorTotal:F2}");
            }

            Console.WriteLine($"\nRelatório exportado como '{nomeArquivo}'.");
            Console.ReadKey();
        }
    }
}
