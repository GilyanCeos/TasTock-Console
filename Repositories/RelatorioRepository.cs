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
            string inicio = dataInicio.ToString("yyyy-MM-dd");
            string fim = dataFim.ToString("yyyy-MM-dd");
            string nomeArquivo = $"relatorio_{periodo.ToLower()}_{inicio}_a_{fim}.csv";

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
