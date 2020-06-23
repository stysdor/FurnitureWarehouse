using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Interface
{
    public interface IOrderRepository
    {
        bool AddOrder(Order order);
        List<Order> GetOrders();



    }
}
