using System;
using System.Collections.Generic;
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
        Indicacao = 1,
        Jornal = 2,
        Internet = 3,
        Outros 
    }
}
