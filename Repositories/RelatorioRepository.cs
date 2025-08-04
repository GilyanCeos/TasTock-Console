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
    }
}
