using TasTock.Services;

namespace TasTock
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ItemService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========== TasTock ===========");
                Console.WriteLine("[1] Cadastrar novo item");
                Console.WriteLine("[2] Listar, filtrar e exportar");
                Console.WriteLine("[3] Remover item");
                Console.WriteLine("[4] Calcular total");
                Console.WriteLine("[0] Sair");
                Console.WriteLine("================================");
                Console.Write("Escolha: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1": service.Cadastrar(); break;
                    //case "2": service.Editar(); break;
                    case "2": service.Listar(); break; // listar com filtros e ordenação
                    case "3": service.Remover(); break;
                    case "4": service.Calcular(); break;
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
