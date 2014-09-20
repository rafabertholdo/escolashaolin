using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence.EntityFramework
{
    public class DynamicEFRepository : BaseEFRepository
    {
        protected override IUnitOfWork UnitOfWork
        {
            get
            {
                return _context as IUnitOfWork;
            }
        }       

        private readonly IUnitOfWork _context;

        public DynamicEFRepository(IUnitOfWork context)
        {            
            _context = context;
        }
    }
}
