using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Estoque.Entidade
{
    public class Encomenda
    {       
        
        public int ProdutoCodigo { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        
        public DateTime DataPrevistaEntrega { get; set; }
        public bool Entregue { get; set; }

        public void EntregarEncomenda(int QuantidadeEntregue)
        {
            throw new NotImplementedException();
        }
    }
}
