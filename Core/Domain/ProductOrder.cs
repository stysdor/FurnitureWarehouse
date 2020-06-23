using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ProductOrder
    {
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ProductOrder;
            if (t == null)
                return false;
            if (Order.Id == t.Order.Id && Product.Id == t.Product.Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (Order.Id + "|" + Product.Id).GetHashCode();
        }
    }
}
