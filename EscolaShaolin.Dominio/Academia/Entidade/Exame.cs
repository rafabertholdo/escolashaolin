using EscolaShaolin.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Dominio.Academia.Entidade
{
    public class Exame : BaseEntity
    {        
        public DateTime Data { get; set; }

        public List<InscricaoExame> Inscricoes { get; set; }

        /// <summary>
        /// Monta a incrição de alunos que podem fazer o proximo exame de acordo com o tempo de treinamento
        /// </summary>
        /// <returns></returns>
        public void CriaSugestaoAlunosParaExame()
        {
            throw new NotImplementedException();
        }        

        /// <summary>
        /// Gera contas a receber para o mes da data do exame, com o valor do exame de cada incrito
        /// </summary>
        /// <returns></returns>
        public bool GerarContasReber()
        {
            throw new NotImplementedException();
        }
    }
}
