using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Aluno : Pessoa
    {        
        public StatusAluno Status { get; set; }
        public string StatusOutros {get;set;}
        public Sexo Sexo { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }

        public DateTime DataNascimento { get; set; }
        public int Idade { get{
            int idade = DateTime.Today.Year - DataNascimento.Year;
            if (DateTime.Today < DataNascimento.AddYears(idade)) idade--;

            return idade;
        } }
        public string Identidade { get; set; }
        
        public string ContatoEmergencia { get; set; }
        public ComoConheceuEscola? ComoConheceuEscola { get; set; }
        //O aluno que indica ganha 50% de desconto no proximo mês
        public Aluno AlunoQueIndicou { get; set; }
        public Guid? AlunoQueIndicouId { get; set; }
        public string ComoConheceuEscolaOutros {get;set;}
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string NomeResponsavel { get; set; }
        public string Email { get; set; }        
        public DateTime? ValidadeCarteira { get; set; }
        public DateTime? DataMatricula { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public Plano Plano { get; set; }        
        public Guid? PlanoId { get; set; }

        /// <summary>
        /// Regitra que o aluno esteve presente no local de treinamento
        /// </summary>
        /// <param name="Dia"></param>
        /// <param name="LocalTreinamento"></param>
        /// <returns></returns>
        public string RegistrarPresenca(DayOfWeek Dia, LocalTreinamento LocalTreinamento)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Quita a conta a receber referente ao plano
        /// </summary>
        /// <param name="plano"></param>
        /// <param name="mesAnoReferencia"></param>
        public void PagarMensalidade(Plano plano, DateTime mesAnoReferencia)
        {
            throw new NotImplementedException();
        }     
    }    

    public enum StatusAluno
    {
        [Description("Ativo")]
        Ativo = 1,
        [Description("Desistente")]
        Desistente = 2,
        [Description("Bloqueado")]
        Bloqueado = 3,
        [Description("Ex Aluno")]
        ExAluno = 4,
        [Description("Outros")]
        Outros = 5
    }

    public enum Sexo
    {
        [Description("Masculino")]
        Masculino = 'M',
        [Description("Feminino")]
        Feminino = 'F'
    }

    public enum EstadoCivil
    {
        [Description("Solteiro")]
        Solteiro = 1,
        [Description("Casado")]
        Casado = 2,
        [Description("Separado")]
        Separado = 3,
        [Description("Divorciado")]
        Divorciado = 4
    }

    public enum ComoConheceuEscola
    {
        [Description("Indicacão")]
        Indicacao = 1,
        [Description("Jornal")]
        Jornal = 2,
        [Description("Internet")]
        Internet = 3,
        [Description("Outros")]
        Outros 
    }
}
