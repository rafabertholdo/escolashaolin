using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence
{
    /// <summary>
    /// ALlows persisting of a simple integer collection.
    /// </summary>
    [ComplexType]
    public class PersistableStringCollection : PersistableScalarCollection<string>
    {
        public PersistableStringCollection()
            : base()
        {

        }
        public PersistableStringCollection(List<string> data)
            : base(data)
        {
        }

        protected override string ConvertSingleValueToRuntime(string rawValue)
        {
            return rawValue;
        }

        protected override string ConvertSingleValueToPersistable(string value)
        {
            return value;
        }
    }
}
