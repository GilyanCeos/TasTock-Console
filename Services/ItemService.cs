using TasTock.Models;
using TasTock.Repositories;

namespace TasTock.Services
{
    public class ItemService
    {
        private readonly ItemRepository _repo;

        public ItemService()
        {
            _repo = new ItemRepository();
        }

        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("==== CADASTRAR NOVO ITEM ====");

            Console.Write("Nome do item: ");
            string? nome = Console.ReadLine();

            Console.Write("Quantidade: ");
            int.TryParse(Console.ReadLine(), out int qtd);

            Console.Write("Preço unitário: ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);

            var item = new Item
            {
                Nome = nome ?? "",
                Quantidade = qtd,
                PrecoUnitario = preco
            };

            _repo.Adicionar(item);
            Console.WriteLine("Item cadastrado com sucesso.");
            Console.ReadKey();
        }

        public void Editar()
        {
            Console.Clear();
            Listar();
            Console.Write("\nInforme o ID do item a editar: ");
            int.TryParse(Console.ReadLine(), out int id);

            var item = _repo.BuscarPorId(id);
            if (item == null)
            {
                Console.WriteLine("Item não encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Novo nome ({item.Nome}): ");
            var nome = Console.ReadLine();
            item.Nome = string.IsNullOrWhiteSpace(nome) ? item.Nome : nome;

            Console.Write($"Nova quantidade ({item.Quantidade}): ");
            if (int.TryParse(Console.ReadLine(), out int qtd)) item.Quantidade = qtd;

            Console.Write($"Novo preço unitário ({item.PrecoUnitario}): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco)) item.PrecoUnitario = preco;

            _repo.Atualizar(item);
            Console.WriteLine("Item atualizado.");
            Console.ReadKey();
        }

        public void Remover()
        {
            Console.Clear();
            Listar();
            Console.Write("\nInforme o ID do item a remover: ");
            int.TryParse(Console.ReadLine(), out int id);

            _repo.Remover(id);
            Console.WriteLine("Item removido.");
            Console.ReadKey();
        }

        public void Calcular()
        {
            Console.Clear();
            var itens = _repo.Listar();

            if (!itens.Any())
            {
                Console.WriteLine("Nenhum item cadastrado.");
            }
            else
            {
                decimal total = itens.Sum(i => i.PrecoUnitario * i.Quantidade);
                Console.WriteLine($"Itens cadastrados: {itens, itens.Count}");
                Console.WriteLine($"Total acumulado (valor x quantidade): R$ {total:F2}");
            }

            Console.ReadKey();
        }

        public void Listar()
         {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== ITENS CADASTRADOS ====\n");

                Console.WriteLine("[1] Listar por nome");
                Console.WriteLine("[2] Listar por faixa de preço");
                Console.WriteLine("[3] Ordenar por data de cadastro");
                Console.WriteLine("[4] Exportar para CSV");
                Console.WriteLine("[0] Voltar");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                 var itens = _repo.Listar();
                 IEnumerable<Item> resultado = itens;

                 switch (opcao)
                {
                    case "1":
                        Console.Write("Digite o nome, ou tecle Enter para lista completa: ");
                        string termo = Console.ReadLine() ?? "";
                        resultado = itens.Where(i => i.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase));
                        break;

                    case "2":
                        Console.Write("Preço mínimo: ");
                        decimal.TryParse(Console.ReadLine(), out decimal min);
                        Console.Write("Preço máximo: ");
                        decimal.TryParse(Console.ReadLine(), out decimal max);
                        resultado = itens.Where(i => i.PrecoUnitario >= min && i.PrecoUnitario <= max);
                        break;

                    case "3":
                        resultado = itens.OrderBy(i => i.CadastradoEm);
                        break;

                    case "4":
                        ExportarCsv(itens);
                        return;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        continue;
                }

                        Console.WriteLine("\n--- RESULTADO ---\n");
                        foreach (var item in resultado)
                        {
                            Console.WriteLine($"ID: {item.Id} | {item.Nome} | {item.Quantidade} un. | R$ {item.PrecoUnitario:F2} | {item.CadastradoEm:dd/MM/yyyy}");
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu de listagem...");
                        Console.ReadKey();
                    }
                }

        private void ExportarCsv(IEnumerable<Item> itens)
        {
            var path = "exportacao_tastock.csv";

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("Id,Nome,Quantidade,PrecoUnitario,CadastradoEm");
                foreach (var item in itens)
                {
                    writer.WriteLine($"{item.Id},{item.Nome},{item.Quantidade},{item.PrecoUnitario},{item.CadastradoEm:yyyy-MM-dd}");
                }
            }

            Console.WriteLine($"\nExportação concluída: {path}");
            Console.ReadKey();
        }
    }
}