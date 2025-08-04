using TasTock.Repositories;
using TasTock.Services;

namespace TasTock
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();
            var repo = new ItemRepository(context);
            var service = new ItemService(repo);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("TasTock");
                Console.WriteLine("\n[1] Cadastrar novo item");
                Console.WriteLine("[2] Editar");
                Console.WriteLine("[3] Listar, filtrar e exportar");
                Console.WriteLine("[4] Calcular total");
                Console.WriteLine("[5] Remover item");
                Console.WriteLine("[0] Sair");
                Console.Write("\nEscolha: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1": service.Cadastrar(); break;
                    case "2": service.Editar(); break;
                    case "3": service.Listar(); break; // listar com filtros e ordenação
                    case "4": service.Calcular(); break;
                    case "5": service.Remover(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
