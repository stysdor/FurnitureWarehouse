using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime OrderDate { get; set; }

        public virtual IList<ProductOrder> ProductOrders { get; set; }
    }
}
