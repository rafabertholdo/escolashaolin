using EscolaShaolin.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Presenca : BaseEntity
    {        
        public DayOfWeek Dia { get; set; }

        public Aluno Aluno { get; set; }
        public int AlunoCodigo { get; set; }
        

        public LocalTreinamento LocalTreinamento { get; set; }
        public int LocalTreinamentoCodigo { get; set; }
    }
}
