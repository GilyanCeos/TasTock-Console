# TasTock

**TasTock** é um aplicativo de console desenvolvido em C# com .NET 9. Ele integra funcionalidades de gerenciamento de tarefas, controle de estoque e recursos de integração com e-mail e APIs externas. O projeto foi criado para fins de estudo, prototipagem e desenvolvimento de soluções empresariais modulares em ambiente local.

## Funcionalidades

* Controle de Estoque

  * Cadastro de produtos com controle de quantidade e preço
  * Registro de entrada e saída de produtos
  * Geração de relatórios simples

* Integrações Externas

  * Envio de e-mails por SMTP com autenticação segura (MailKit)
  * Consumo de APIs externas (exemplo: consulta de CEP via ViaCEP)

## Tecnologias Utilizadas

* .NET 9
* C# moderno
* Entity Framework Core 9 com SQLite
* MailKit para envio de e-mails
* HttpClient para chamadas a APIs externas

## Instalação

1. Clone o repositório:

```bash
git clone https://github.com/seuusuario/TasTock.git
cd TasTock
```

2. Instale as dependências:

```bash
dotnet restore
dotnet tool install --global dotnet-ef
```

3. Crie o banco de dados:

```bash
dotnet ef database update
```

4. Execute a aplicação:

```bash
dotnet run
```

## Estrutura do Projeto

/Models         -> Modelos de dados (Tarefa, Produto)
/Repositories   -> Repositórios com acesso ao banco
/Services       -> Camada de regras de negócio
/Utils          -> Integrações e funções auxiliares
Program.cs      -> Ponto de entrada da aplicação e menu principal

## Objetivo

O projeto foi criado com fins didáticos e práticos, permitindo que desenvolvedores explorem conceitos fundamentais e avançados em:

Estruturação de código limpo e modular

Acesso e manipulação de banco de dados local com EF Core

Princípios de arquitetura em camadas

Comunicação com serviços externos via rede e SMTP

## Licença

Este projeto está licenciado sob os termos da Licença MIT. Consulte o arquivo [LICENSE](./LICENSE) para mais informações.

## Contato

Desenvolvido por [Gilyan Ceos]
E-mail: gilyan.dev@outlook.com
LinkedIn: https://www.linkedin.com/in/gilyan-santos/