# TasTock

![C#](https://img.shields.io/badge/C%20Sharp-limegreen)
![.NET](https://img.shields.io/badge/.NET%209.0-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-mediumorchid)
![SQLite](https://img.shields.io/badge/SQLite-darkcyan)
![LINQ](https://img.shields.io/badge/LINQ-darkorange)
![Plataforma](https://img.shields.io/badge/Console--App-darkgrey)


## Descrição

**TasTock** é um sistema de console para controle de estoque e vendas, desenvolvido como um projeto de estudo e prática para aprimorar habilidades em C# e .NET 9. O objetivo é proporcionar uma aplicação simples e funcional que abrange conceitos importantes como manipulação de banco de dados, organização em camadas (Repositories, Services), e interação via linha de comando.

O banco de dados SQLite é criado automaticamente na pasta do programa no momento da execução, não sendo necessária configuração manual.


## Funcionalidades principais

- Cadastro, edição e remoção de itens no estoque
- Controle de quantidade e preço unitário dos produtos
- Listagem de itens com filtros por nome, faixa de preço e ordenação por data de cadastro
- Exportação do estoque para arquivo CSV
- Registro e controle de vendas com cálculo de descontos
- Controle financeiro com relatórios por períodos (diário, semanal, mensal e anual)
- Exportação de relatórios financeiros em CSV
- Interface de console simples e intuitiva


## Tecnologias utilizadas

- C# com .NET 9
- Entity Framework Core
- SQLite (banco de dados local criado automaticamente)
- LINQ para consultas e filtragem de dados


## Como executar

1. Tenha o [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) instalado em seu sistema.

2. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    ```

3. Navegue até a pasta do projeto:
    ```bash
    cd TasTock
    ```

4. Execute o projeto com o comando:
    ```bash
    dotnet run
    ```

- O banco de dados SQLite (tasTock.db) será criado automaticamente na pasta do projeto na primeira execução.


## Etapas futuras

[✓] Refatorar o backend como uma API RESTful usando ASP.NET Core

[ ] Criar uma interface mobile responsiva usando .NET MAUI voltada para Android <b>[<i><u>Em desenvolvimento</u></i>]</b>

[ ] Sincronizar dados entre o app e a API via requisições HTTP

[ ] Separar os módulos em camadas independentes para reuso em frontends distintos

[ ] Implementar autenticação e segurança nas rotas da API

[ ] Publicar versão instalável para testes internos em dispositivos Android

## Estrutura do projeto

- Program.cs: Interface principal, menu e fluxo do aplicativo

- AppDbContext.cs:
        Contexto do banco de dados usando Entity Framework Core e SQLite

- Models/Item.cs:
        Modelo para itens de estoque

- Models/Relatorio.cs:
        Modelo para registros financeiros e relatórios

- Repositories/ItemRepository.cs:
        Acesso e manipulação dos dados de itens

- Repositories/RelatorioRepository.cs:
        Acesso e manipulação dos dados de relatórios

- Services/ItemService.cs:
        Lógica de negócio, controle de cadastro, edição, vendas, listagens e relatórios


## Observações

- Projeto criado para fins educativos e prática pessoal.

- A interface atual é exclusivamente via terminal/console.

- Em evolução para uma versão com interface gráfica multiplataforma (MAUI).


## Licença

Este projeto está licenciado sob a [Creative Commons Attribution-NonCommercial 4.0 International](https://creativecommons.org/licenses/by-nc/4.0/deed.pt_BR). Consulte o arquivo [LICENSE](./LICENSE) para mais informações.


## Autoria

- Desenvolvido por [Gilyan Ceos](https://github.com/GilyanCeos)

- LinkedIn: [gilyan-santos](https://linkedin.com/in/gilyan-santos)

- E-mail: gilyan.dev@gmail.com

---

Obrigado por acessar o TasTock!

Fique à vontade para contribuir, sugerir melhorias ou usar o projeto para aprendizado.
