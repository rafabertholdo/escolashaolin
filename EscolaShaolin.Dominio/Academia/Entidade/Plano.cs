using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Plano
    {       
        public float Valor { get; set; }
        public List<QuantidadeModalidade> QuantatidadeModalidades { get; set; }

        public LocalTreinamento LocalTrainamento { get; set; }
        public int LocalTrainamentoCodigo { get; set; }
    }

    public class QuantidadeModalidade
    {
        public int QuantidadeAulasNaSemana { get; set; }
        public Modalidade Modalidade { get; set; }
    }
}
