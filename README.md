# WebAPI ASP.NET / C# / Dapper / SQL Server


Exemplo de WebAPI com ASP.NET Core, Dapper e SQL Server.

- **.NET 8.0**
- **C#**
- **Dapper**
- **SQL Server**
- **Swagger**

## Funcionalidades

- **Criar** um novo registro de funcionario
- **Listar** todos os funcionarios cadastrados
- **Atualizar** as informações de um funcionario
- **Deletar** um funcionario da base de dados

## Endpoints

- ``GET /api/Employees``: Retorna a lista de todos os funcionarios.
- ``GET /api/Employees/{id}``: Retorna um funcionario específico pelo ID.
- ``POST /api/Employees``: Adiciona um novo funcionario.
- ``PUT /api/Employees/{id}``: Atualiza as informações de um funcionario.
- ``DELETE /api/Employees/{id}``: Remove um funcionario pelo ID.