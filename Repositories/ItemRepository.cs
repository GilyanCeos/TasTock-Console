using TasTock.Models;
using System.Collections.Generic;
using System.Linq;

namespace TasTock.Repositories
{
    public class ItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated(); // Cria o banco se não existir
        }

        public List<Item> Listar() => _context.Itens.ToList();

        public Item? BuscarPorId(int id) => _context.Itens.Find(id);

        public void Adicionar(Item item)
        {
            _context.Itens.Add(item);
            _context.SaveChanges();
        }

        public void Atualizar(Item item)
        {
            _context.Itens.Update(item);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var item = BuscarPorId(id);
            if (item != null)
            {
                _context.Itens.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
