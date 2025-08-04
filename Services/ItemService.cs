using TasTock.Models;
using TasTock.Repositories;
using System.Globalization;

namespace TasTock.Services
{
    public class ItemService
    {
        private readonly ItemRepository _repo;
        private readonly RelatorioRepository _repoRelatorio;

        public ItemService(ItemRepository repo)
        {
            _repo = repo;
            _repoRelatorio = new RelatorioRepository();
        }

        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("CADASTRAR NOVO ITEM");

            Console.Write("Nome do item: ");
            string? nome = Console.ReadLine();

            int qtd;
            do
            {
                Console.Write("Quantidade: ");
            } while (!int.TryParse(Console.ReadLine(), out qtd) || qtd < 0);

            decimal preco;
            do
            {
                Console.Write("Preço unitário: ");
            } while (!decimal.TryParse(Console.ReadLine(), out preco) || preco < 0);

            var item = new Item
            {
                Nome = nome?.Trim() ?? "",
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
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            var item = _repo.BuscarPorId(id);
            if (item == null)
            {
                Console.WriteLine("Item não encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Novo nome ({item.Nome}): ");
            var nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
                item.Nome = nome.Trim();

            Console.Write($"Nova quantidade ({item.Quantidade}): ");
            if (int.TryParse(Console.ReadLine(), out int qtd) && qtd >= 0)
                item.Quantidade = qtd;

            Console.Write($"Novo preço unitário ({item.PrecoUnitario}): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco) && preco >= 0)
                item.PrecoUnitario = preco;

            _repo.Atualizar(item);

            Console.WriteLine("Item atualizado.");
            Console.ReadKey();
        }

        public void Remover()
        {
            Console.Clear();
            Listar();
            Console.Write("\nInforme o ID do item a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            _repo.Remover(id);

            Console.WriteLine("Item removido.");
            Console.ReadKey();
        }

        public void Listar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ITENS CADASTRADOS\n");
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
                        Console.Write("Digite o nome (ou Enter para lista completa): ");
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

                Console.WriteLine("\nRESULTADO\n");
                foreach (var item in resultado)
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Nome} | {item.Quantidade} un. | {item.PrecoUnitario.ToString("C", new CultureInfo("pt-BR"))} | {item.CadastradoEm:dd/MM/yyyy}");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }

        private void ExportarCsv(IEnumerable<Item> itens)
        {
            string path = "exportacao_tastock.csv";

            using var writer = new StreamWriter(path);
            writer.WriteLine("Id,Nome,Quantidade,PrecoUnitario,CadastradoEm");

            foreach (var item in itens)
            {
                writer.WriteLine($"{item.Id},{item.Nome},{item.Quantidade},{item.PrecoUnitario},{item.CadastradoEm:yyyy-MM-dd}");
            }

            Console.WriteLine($"\nExportação concluída: {path}");
            Console.ReadKey();
        }

        public void Calcular()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Controle de caixa\n");
                Console.WriteLine("[1] Exibir total acumulado atual");
                Console.WriteLine("[2] Relatórios por período");
                Console.WriteLine("[0] Voltar");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        ExibirTotalAtual();
                        break;
                    case "2":
                        ExibirRelatorios();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ExibirTotalAtual()
        {
            Console.Clear();
            var itens = _repo.Listar();

            if (!itens.Any())
            {
                Console.WriteLine("Nenhum item cadastrado.");
            }
            else
            {
                decimal totalAtual = itens.Sum(i => i.PrecoUnitario * i.Quantidade);

                Console.WriteLine($"\nItens cadastrados: {itens.Count}\n");
                foreach (var item in itens)
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Nome} | {item.Quantidade} un. | {item.PrecoUnitario.ToString("C", new CultureInfo("pt-BR"))}");
                }

                var ultimoRegistro = _repoRelatorio.Listar()
                    .OrderByDescending(r => r.Data)
                    .FirstOrDefault();

                Console.WriteLine("\nTotais de Estoque\n");

                if (ultimoRegistro != null)
                {
                    Console.WriteLine($"Total ANTERIOR registrado: {ultimoRegistro.ValorAntes.ToString("C", new CultureInfo("pt-BR"))}");
                    Console.WriteLine($"Total registrado APÓS última venda/reposição: {ultimoRegistro.ValorDepois.ToString("C", new CultureInfo("pt-BR"))}");
                }
                else
                {
                    Console.WriteLine("Nenhum registro anterior encontrado.");
                }

                Console.WriteLine($"\nTotal ATUAL (calculado): {totalAtual.ToString("C", new CultureInfo("pt-BR"))}");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void ExibirRelatorios()
        {
            Console.Clear();
            Console.WriteLine("RELATÓRIOS POR PERÍODO");
            Console.WriteLine("[1] Diário");
            Console.WriteLine("[2] Semanal");
            Console.WriteLine("[3] Mensal");
            Console.WriteLine("[4] Anual");
            Console.WriteLine("[0] Voltar");

            Console.Write("\nEscolha uma opção: ");
            string? opcao = Console.ReadLine();

            DateTime hoje = DateTime.Today;
            DateTime dataInicio, dataFim;
            List<Relatorio> relFiltrados;
            string tipo;

            switch (opcao)
            {
                case "1": // Diário
                    dataInicio = hoje;
                    dataFim = hoje;
                    tipo = "diario";
                    break;

                case "2": // Semanal
                    dataInicio = hoje.AddDays(-6);
                    dataFim = hoje;
                    tipo = "semanal";
                    break;

                case "3": // Mensal
                    dataInicio = new DateTime(hoje.Year, hoje.Month, 1);
                    dataFim = dataInicio.AddMonths(1).AddDays(-1);
                    tipo = "mensal";
                    break;

                case "4": // Anual
                    dataInicio = new DateTime(hoje.Year, 1, 1);
                    dataFim = new DateTime(hoje.Year, 12, 31);
                    tipo = "anual";
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    return;
            }

            relFiltrados = _repoRelatorio.ListarPorPeriodo(dataInicio, dataFim);

            Console.Clear();
            Console.WriteLine($"RELATÓRIOS DE {dataInicio:dd/MM/yyyy} A {dataFim:dd/MM/yyyy}\n");

            if (!relFiltrados.Any())
            {
                Console.WriteLine("Nenhum relatório encontrado para o período.");
            }
            else
            {
                foreach (var rel in relFiltrados)
                {
                    Console.WriteLine($"{rel.Tipo} | {rel.NomeItem} | {rel.Quantidade} un. | R$ {rel.ValorTotal:F2} | {rel.Data:dd/MM/yyyy}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();

            // Pergunta se deseja exportar após mostrar
            Console.Write("\nDeseja exportar este relatório? (S/N): ");
            var exportar = Console.ReadLine();
            if (!string.IsNullOrEmpty(exportar) && exportar.Trim().ToUpper() == "S")
            {
                _repoRelatorio.ExportarRelatorios(relFiltrados, tipo, dataInicio, dataFim);
            }
        }

        public void RealizarVenda()
        {
            Console.Clear();
            Console.WriteLine("REALIZAR VENDA\n");

            Console.Write("Nome do produto: ");
            string nomeProduto = Console.ReadLine() ?? "";

            var itens = _repo.Listar();
            var item = itens.FirstOrDefault(i => i.Nome.Equals(nomeProduto, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine("Produto não encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Quantidade a vender: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidadeVendida) || quantidadeVendida <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                Console.ReadKey();
                return;
            }

            if (quantidadeVendida > item.Quantidade)
            {
                Console.WriteLine($"Quantidade insuficiente em estoque. Disponível: {item.Quantidade}");
                Console.ReadKey();
                return;
            }

            Console.Write("Desconto (%) [0-100]: ");
            if (!int.TryParse(Console.ReadLine(), out int desconto) || desconto < 0 || desconto > 100)
            {
                Console.WriteLine("Desconto inválido.");
                Console.ReadKey();
                return;
            }

            // Calcular totais
            decimal valorBruto = quantidadeVendida * item.PrecoUnitario;
            decimal valorDesconto = valorBruto * desconto / 100m;
            decimal valorFinal = valorBruto - valorDesconto;

            // Total antes da venda
            decimal totalAntes = itens.Sum(i => i.PrecoUnitario * i.Quantidade);

            // Atualizar estoque
            item.Quantidade -= quantidadeVendida;
            _repo.Atualizar(item);

            // Total depois da venda
            decimal totalDepois = _repo.Listar().Sum(i => i.PrecoUnitario * i.Quantidade);

            // Registrar no relatório
            var relatorio = new Relatorio
            {
                NomeItem = item.Nome,
                Tipo = "Saída",
                Quantidade = quantidadeVendida,
                ValorTotal = valorFinal,
                ValorAntes = totalAntes,
                ValorDepois = totalDepois,
                Data = DateTime.Now
            };
            _repoRelatorio.Registrar(relatorio);

            // Exibir resumo
            var cultura = new CultureInfo("pt-BR");

            Console.WriteLine($"\nVenda registrada com sucesso!");
            Console.WriteLine($"Valor bruto: {valorBruto.ToString("C", cultura)}");
            Console.WriteLine($"Desconto aplicado: {valorDesconto.ToString("C", cultura)} ({desconto}%)");
            Console.WriteLine($"Valor final da venda: {valorFinal.ToString("C", cultura)}");

            Console.WriteLine($"\nTotal em estoque ANTES da venda: {totalAntes.ToString("C", cultura)}");
            Console.WriteLine($"Total em estoque DEPOIS da venda: {totalDepois.ToString("C", cultura)}");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
