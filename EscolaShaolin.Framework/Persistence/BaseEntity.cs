using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence
{
    public abstract class BaseEntity : IEntity<BaseEntity>, IComparable
    {    

        public Guid Id
        {
            get;
            set;
        }

        public DateTime InsertDate
        {
            get;
            set;
        }

        public DateTime LastUpdateDate
        {
            get;
            set;
        }

        #region IEntity<BaseEntity> Members

        /// <summary>
        /// Entities compare by identity, not by attributes.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>true if the identities are the same, regardles of other attributes.</returns>
        public bool SameIdentityAs(BaseEntity other)
        {
            return other != null && this.Id == other.Id;
        }
        #endregion

        #region Object's override

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is BaseEntity)
            {
                BaseEntity aux = (BaseEntity)obj;
                result = aux.Id == this.Id && aux.InsertDate == this.InsertDate && aux.LastUpdateDate == this.LastUpdateDate;
            }
            return result;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity first, BaseEntity second)
        {
            if ((((object)first) == null) && (((object)second) == null))
                return true;
            else if ((((object)first) == null))
                return false;

            return first.SameIdentityAs(second);
        }

        public static bool operator !=(BaseEntity first, BaseEntity second)
        {
            return !(first == second);
        }
        #endregion        

        public virtual int CompareTo(object obj)
        {
            if (obj is BaseEntity)
            {
                return Id.CompareTo(((BaseEntity)obj).Id);
            }
            else
            {
                return -1;
            }
        }

        public BaseEntity ShallowCopy()
        {
            return (BaseEntity)this.MemberwiseClone();
        }

        public virtual object[] GetKeys()
        {
            return new object[] { this.Id };
        }
    }
}
