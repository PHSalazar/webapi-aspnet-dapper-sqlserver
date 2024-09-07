using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApiDapper.Models;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly string _connectionString;
        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            const string sql = "SELECT * FROM Funcionarios";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var funcionarios = await sqlConnection.QueryAsync<Employee>(sql);
                return Ok(funcionarios);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            const string sql = "SELECT * FROM Funcionarios WHERE Id = @id";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                // Passando o ID como parametro
                var functionarios = await sqlConnection.QuerySingleOrDefaultAsync<Employee>(sql, new {Id = id});

                // Retornar Não encontrado se não encontrar o funcionário com ID
                if (functionarios is null)
                {
                    return NotFound();
                }

                return Ok(functionarios);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee model)
        {

            var funcionario = new Employee(model.Nome, model.Sexo, model.Matricula, model.Cargo, model.Salario, model.DataAdmissao);
            var parameters = new
            {
                funcionario.Nome,
                funcionario.Sexo,
                funcionario.Matricula,
                funcionario.Cargo,
                funcionario.Salario,
                funcionario.DataAdmissao,
                funcionario.EstaAtivo
            };

            var sqlChecarExisteMatricula = "SELECT * FROM Funcionarios WHERE Matricula = @Matricula";
            
            const string sql = "INSERT INTO Funcionarios (Nome, Sexo, Matricula, Cargo, Salario, DataAdmissao, EstaAtivo) VALUES (@Nome, @Sexo, @Matricula, @Cargo, @Salario, @DataAdmissao, @EstaAtivo)";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                // Verificando se existe matrcula
                var existeMatricula = await sqlConnection.ExecuteScalarAsync<int>(sqlChecarExisteMatricula, new { Matricula = parameters.Matricula });
                if (existeMatricula == 1)
                    return Conflict("A matricula " + parameters.Matricula + " já existe no banco de dados.");
                
                var cmdSql = await sqlConnection.ExecuteAsync(sql, parameters);
                return Ok(funcionario.Id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee model)
        {
            var funcionario = new Employee(model.Nome, model.Sexo, model.Matricula, model.Cargo, model.Salario, model.DataAdmissao);
            var parameters = new
            {
                id,
                funcionario.Nome,
                funcionario.Sexo,
                funcionario.Matricula,
                funcionario.Cargo,
                funcionario.Salario,
                funcionario.EstaAtivo
            };

            const string sql = "UPDATE Funcionarios SET Nome = @Nome, Sexo = @Sexo, Matricula = @Matricula, Cargo = @Cargo, Salario = @Salario, EstaAtivo = @EstaAtivo WHERE Id = @id";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(sql, parameters);
                return Ok();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            const string sql = "UPDATE Funcionarios SET EstaAtivo = 0 WHERE Id = @id";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(sql, new {Id = id});
                return Ok();
            }
        }
    }
}
