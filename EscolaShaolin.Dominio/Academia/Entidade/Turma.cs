using EscolaShaolin.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Turma : BaseEntity
    {   
        public int ModalidadeCodigo { get; set; }
        public Modalidade Modalidade { get; set; }

        public int ProfissionalCodigo { get; set; }
        public Profissional Profissional { get; set; }
        
        public int LocalTreinamentoCodigo { get; set; }        
        public LocalTreinamento LocalTreinamento { get; set; }

        public List<Horario> Horarios { get; set; }
        public List<Aluno> Alunos { get; set; }
    }    
}
