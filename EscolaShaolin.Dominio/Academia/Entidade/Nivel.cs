using EscolaShaolin.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Nivel : BaseEntity
    {
        public string Nome { get; set; }

        /// <summary>
        /// Ordem numeral do nível
        /// ex: Primeiro Nível 1, Estrela vemelha = 3
        /// </summary>
        public int Ordem { get; set; }
        public float ValorExame { get; set; }
        public float NotaMinimaAprovacao { get; set; }
        
        /// <summary>
        /// Número mínimo de meses de treinamento, a partir do ultimo exame, necessários para fazer o exame para este nivel
        /// </summary>
        public int NumeroMesesMinimo { get; set; }
        public Modalidade Modalidade { get; set; }
        public int ModalidadeCodigo { get; set; }

        /// <summary>
        /// Quisitos que serão avaliados no exame
        /// </summary>
        public List<Quesito> Quisitos { get; set; }
    }
}
