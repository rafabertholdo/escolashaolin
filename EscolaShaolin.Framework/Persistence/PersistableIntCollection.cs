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
    public class PersistableIntCollection : PersistableScalarCollection<int>
    {
        public PersistableIntCollection()
            : base()
        {

        }
        public PersistableIntCollection(List<int> data)
            : base(data)
        {
        }
        protected override int ConvertSingleValueToRuntime(string rawValue)
        {
            return int.Parse(rawValue);
        }

        protected override string ConvertSingleValueToPersistable(int value)
        {
            return value.ToString();
        }
    }
}
