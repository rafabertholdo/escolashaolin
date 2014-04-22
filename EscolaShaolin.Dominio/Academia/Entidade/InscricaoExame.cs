using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class InscricaoExame
    {
        public Aluno Aluno { get; set; }
        public int AlunoCodigo { get; set; }
        
        public Exame Exame { get; set; }
        public int ExameCodigo { get; set; }
        
        public Nivel Nivel { get; set; }
        public int NivelCodigo { get; set; }
        
        public bool ContaReceberGerada { get; set; }
        public float NotaFinal { get; set; }
        public bool Aprovado
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<Nota> Notas { get; set; }
    }

}
