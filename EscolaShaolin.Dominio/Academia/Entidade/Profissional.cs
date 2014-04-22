using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Profissional
    {        
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoProfissional TipoProfissional { get; set; }
    }

    public enum TipoProfissional
    {
        Instrutor = 1,
        Professor = 2,        
        Mestre = 3
    }
}
