using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Financeiro.Entidade
{
    public class Conta
    {        
        public TipoConta Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }        
        public float Valor { get; set; }
        public bool Quitada { get; set; }
        public Pessoa Pessoa { get; set; }
        public int PessoaCodigo { get; set; }
        public CentroCustos CentroCustos { get; set; }
        public int CentroCustosCodigo { get; set; }

        /// <summary>
        /// Lança varias a mesma conta com vencimentos por mês de acordo com o número de repetições
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="numeroRepeticoes"></param>
        public static void LancarConta(Conta conta, int numeroRepeticoes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Busca todos os alunos ativos e gera contas a receber para o mes de refencia
        /// </summary>
        /// <param name="MesReferencia"></param>
        public static void GerarContasAlunos(DateTime MesReferencia)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Quita a conta
        /// </summary>
        public void QuitarConta()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Muda o valor da conta para o valor pago, quita conta, cria uma nova conta com o valor desta conta menos o valor pago 
        /// para a data de vencimento especificada
        /// </summary>
        /// <param name="valor"></param>
        public void ReceberParcelaConta(float valorPago, DateTime DataProximoVencimento )
        {
            throw new NotImplementedException();
        }
    }

    public enum TipoConta
    {
        Receber = 1,
        Pagar = 2
    }
}
