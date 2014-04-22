using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Estoque.Entidade
{
    public class Movimentacao
    {        
        public OperacaoMovimentacao Operacao { get; set; }
        
        public Produto Produto { get; set; }
        public int ProdutoCodigo { get; set; }
        public float ValorUnitario { get; set; }
        
        public int Quantidade { get; set; }
        public bool Registrada { get; set; }

        public void RegistrarMovimentacao()
        {
            throw new NotImplementedException();
        }
    }

    public enum OperacaoMovimentacao
    {
        Entrada = 1,
        Saida = 2
    }
}
