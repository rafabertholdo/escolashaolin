using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Turma
    {
        public int Codigo { get; set; }
        public int ModalidadeCodigo { get; set; }
        public Modalidade Modalidade { get; set; }
        [Flags]
        public Enum Turno { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public List<DayOfWeek> Dias { get; set; }
    }

    public enum Turno{
        Manha = 1,
        Tarde = 2,
        Noite = 4,
        Livre = 8
    }
}
