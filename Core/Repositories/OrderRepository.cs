using Core.Domain;
using Core.NHibernate;
using Core.Repositories.Interface;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
            NHibernateBase nHibernate = new NHibernateBase();
            nHibernate.Initialize();
        }

        public bool AddOrder(Order order)
        {
            bool isSucceed = false;
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        session.Save(order);
                        transaction.Commit();
                        isSucceed = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
                //foreach (var item in order.ProductOrders)
                //{
                //    item.Order = order;
   
                //    using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                //    {
                //        try
                //        {
                //            session.Save(item);
                //            transaction.Commit();
                //            isSucceed = true;
                //        }
                //        catch (Exception e)
                //        {
                //            transaction.Rollback();
                //        }
                //    }
                //}
            }
            return isSucceed;
        }

        public List<Order> GetOrders()
        {
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                var orders = session.Query<Order>().ToList();
                foreach (var item in orders)
                {
                    NHibernateUtil.Initialize(item.ProductOrders);
                }
                return orders;
            }
        }
    }
}
