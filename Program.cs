using Microsoft.EntityFrameworkCore;
using TasTock.Models;
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
            using var db = new AppDbContext();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════════════╗");
                Console.WriteLine("║        TASTOCK - Console Version 1.0        ║");
                Console.WriteLine("╚═════════════════════════════════════════════╝\n");
                Console.WriteLine("───────────────────────────────────────────────");
                Console.WriteLine(""+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("───────────────────────────────────────────────");
                //Console.WriteLine("TasTock-Console");
                Console.WriteLine("\n[1] Cadastrar item");
                Console.WriteLine("[2] Editar item");
                Console.WriteLine("[3] Remover item");
                Console.WriteLine("[4] Realizar venda");
                Console.WriteLine("[5] Controle de estoque");
                Console.WriteLine("[6] Controle de caixa");
                Console.WriteLine("[0] Sair");
                Console.Write("\nEscolha: \n");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1": service.Cadastrar(); break;
                    case "2": service.Editar(db); break;
                    case "3": service.Remover(db); break;
                    case "4": service.RealizarVenda(); break;
                    case "5": service.Listar(); break; // com filtros e ordenação
                    case "6": service.Calcular(); break;
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