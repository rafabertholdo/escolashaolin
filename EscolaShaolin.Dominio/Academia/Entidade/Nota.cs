using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Nota
    {
        public Quesito Quisito { get; set; }
        public float ValorNota { get; set; }

        public Profissional Avaliador { get; set; }
        public int AvaliadorCodigo { get; set; }
    }
}
