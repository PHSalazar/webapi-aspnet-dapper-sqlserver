using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApiDapper.Models
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public char Sexo { get; set; }
        public string Matricula { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool EstaAtivo { get; set; }

        protected Employee() { }

        public Employee(string nome, char sexo, string matricula, string cargo, decimal salario, DateTime dataAdmissao)
        {
            Nome = nome;
            Sexo = sexo;
            Matricula = matricula;
            Cargo = cargo;
            Salario = salario;
            DataAdmissao = dataAdmissao;
            EstaAtivo = true;
        }

    }
}