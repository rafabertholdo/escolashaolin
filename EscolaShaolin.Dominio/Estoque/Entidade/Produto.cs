using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Estoque.Entidade
{
    public class Produto
    {        
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public int EstoqueAtual { get; set; }
        
        public Fornecedor PrincipalFornecedor { get; set; }
        public int PrincipalFornecedorCodigo { get; set; }        
    }
}
