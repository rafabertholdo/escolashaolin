using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Estoque.Entidade
{
    public class ProdutoVenda
    {        
        public int ProdutoCodigo { get; set; }
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
    }
}
