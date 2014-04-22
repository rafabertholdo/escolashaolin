using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Estoque.Entidade
{
    public class Fornecedor : Pessoa
    {        
        public string NomeFantasia { get; set; }        
        public string Fax { get; set; }        
        public string InscricaoEstadual { get; set; }        
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Observacoes { get; set; }
        public LinhaProduto LinhaProduto { get; set; }
    }

    public enum LinhaProduto
    {
        Alimentacao = 1,
        Vestuario = 2,
    }
}

