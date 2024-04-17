## CruiseControl API 🌐

## ⚙️ Status: Pode ser melhorado.

### Este projeto é uma API Web que implementa um sistema de Locação de Carros.

### Regras do Negócio.

### Cadastro de Clientes: Os clientes podem se cadastrar no sistema, fornecendo informações como nome, endereço, número de telefone, etc.

### Cadastro de Carros: Os carros podem ser cadastrados no sistema com detalhes como marca, modelo, ano, categoria, disponibilidade, etc.

### Reservas de Carros: Os clientes podem fazer reservas de carros disponíveis para datas específicas. As reservas podem ser confirmadas ou rejeitadas com base na disponibilidade dos carros.

### Gerenciamento de Reservas: Os funcionários podem gerenciar as reservas, visualizando, atualizando ou cancelando reservas existentes.

### Cobranças: As reservas confirmadas podem gerar cobranças para os clientes. O sistema pode integrar com um serviço de cobrança, como o Asaas, para gerar e enviar cobranças aos clientes.

### Pagamentos: Os clientes podem fazer pagamentos das cobranças pendentes através do sistema. Os pagamentos podem ser processados usando diferentes métodos, como cartão de crédito, boleto bancário, PIX, etc.

### Integração com o Google Calendar: O sistema pode integrar com o Google Calendar para agendar datas de retirada e devolução de carros e enviar lembretes aos clientes.

### Envio de Emails: O sistema pode enviar emails de confirmação de reservas, lembretes de pagamento e outras notificações aos clientes.


### Funcionalidades 🖥️ 


- ☑ CRUD Carro
- ☑ CRUD Cliente
- ☑ CRUD Custo
- ☑ CRUD Pagamentos
- ☑ CRUD Reservas
- ☑ CRUD Notificação 
- ☑ Confirmação de Reserva (Email + Google Agenda).
- ☑ Background Service rodando e notificando no dia anterior.
- ☑ Autenticação e Autorização.
- ☑ Asaas como meio de pagamento.
  

### Tecnologias utilizadas 💡


- ASP.NET Core 8: framework web para desenvolvimento de aplicações .NET
- Entity Framework Core: persistência e consulta de dados.
- SQL Server: banco de dados relacional.
- Mensageria com RabbitMQ.
- API Gmail
- API Google Calendar
- API Asaas 

### Padrões, conceitos e arquitetura utilizada 📂


- ☑ Fluent Validation
- ☑ Padrão Repository
- ☑ Middleware (Lidar com exceções)
- ☑ InputModel, ViewModel
- ☑ DTO’s 
- ☑ IEntityTipeConfiguration 
- ☑ Sql Server 
- ☑ Unit Of Work
- ☑ HostedService
- ☑ Domain Event
- ☑ CQRS
- ☑ Teste Unitários
- ☑ Arquitetura Limpa
- ☑ Microsserviços
- ☑ RabbitMQ


 
## Instalação

### Requisitos

Antes de começar, verifique se você tem os seguintes requisitos instalados:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0): A versão do .NET Framework necessária para executar a API.
- [SQL Server](https://www.microsoft.com/en-us/sql-server): O banco de dados utilizado para armazenar os dados.

### Clone

Clone o repositório do GitHub:

```bash
git clone https://github.com/[seu-usuário]/CruiseControl.API.git
```

### Navegue até a pasta do projeto:

```bash
cd CruiseControl.API
```

### Abra o projeto na sua IDE de preferência (a IDE utilizada para desenvolvimento foi o Visual Studio)

### Restaure os pacotes:

```bash
dotnet restore
```

### Configure o banco de dados:

1. Abra o arquivo `appsettings.json`.
2. Altere as configurações do banco de dados para corresponder ao seu ambiente.
3. Faça as devidas alterações para as funcionalidades: Email, Google Calendar e Gateway Asaas.

### Execute a API:

Para executar a API, use o seguinte comando:

```bash
dotnet run
```

### Lembre-se de substituir [seu-usuário] pelo seu nome de usuário do GitHub.
### Lembre-se de fazer as devidas alterações para o uso correto das API's do Gmail, Asaas e Google Calendar.

Este projeto foi criado para fins didáticos e não abrange todas as regras e conceitos necessários de uma aplicação real em produção.*
