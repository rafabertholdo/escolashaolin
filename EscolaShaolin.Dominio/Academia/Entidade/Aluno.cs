using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Aluno : Pessoa
    {        
        public StatusAlulo Status { get; set; }
        public string StatusOutros {get;set;}
        public Sexo Sexo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }

        public DateTime DataNascimento { get; set; }
        public int Idade { get{
            int idade = DateTime.Today.Year - DataNascimento.Year;
            if (DateTime.Today < DataNascimento.AddYears(idade)) idade--;

            return idade;
        } }
        public string Identidade { get; set; }
        
        public string ContatoEmergencia { get; set; }
        public ComoConheceuEscola ComoConheceuEscola {get;set;}
        public string ComoConheceuEscolaOutros {get;set;}
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string NomeResponsavel { get; set; }
        public string Email { get; set; }        
        public DateTime ValidadeCarteira { get; set; }
        public DateTime DataMatricula { get; set; }
        public DateTime DataCancelamento { get; set; }
        public Plano Plano { get; set; }

        public string RegistrarPresenca(DayOfWeek Dia)
        {
            throw new NotImplementedException();
        }

        public void PagarMensalidade(Plano plano, DateTime mesAnoReferencia)
        {
            throw new NotImplementedException();
        }     
    }    

    public enum StatusAlulo
    {
        Ativo = 1,
        Desistente = 2,
        Bloqueado = 3,
        ExAluno = 4,
        Outros = 5
    }

    public enum Sexo
    {
        Masculino = 'M',
        Feminio = 'F'
    }

    public enum EstadoCivil
    {
        Solteiro = 1,
        Casado = 2,
        Separado = 3,
        Divorciado = 4
    }

    public enum ComoConheceuEscola
    {
        Amigo = 1,
        Jornal = 2,
        Internet = 3,
        Outros 
    }
}
