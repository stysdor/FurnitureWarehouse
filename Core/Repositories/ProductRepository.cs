using Core.Domain;
using Core.NHibernate;
using Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public ProductRepository()
        {
            NHibernateBase nHibernate = new NHibernateBase();
            nHibernate.Initialize();
        }

        public List<Category> GetCategories()
        {
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                var categories = session.Query<Category>().ToList();
                return categories;
            }
        }

        public List<Color> GetColors()
        {
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                var colors = session.Query<Color>().ToList();
                return colors;
            }
        }

        public List<Product> GetProducts()
        {

            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                var products = session.Query<Product>().ToList();
                return products;
            }
        }

        public bool AddProduct(Product product)
        {
            bool isSucceed = false;
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        session.Save(product);
                        transaction.Commit();
                        isSucceed = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                    }
                }
            }
            return isSucceed;
        }

        public bool DeleteProduct(Product product)
        {
            bool isSucceed = false;
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        session.Delete(product);
                        transaction.Commit();
                        isSucceed = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                    }
                }
            }
            return isSucceed;
        }

        public bool EditProduct(Product product)
        {
            bool isSucceed = false;
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        session.Update(product);
                        transaction.Commit();
                        isSucceed = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                    }
                }
            }
            return isSucceed;
        }

        public Product GetProduct(int id)
        {
            Product product = new Product();
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        product = session.Query<Product>()
                            .Where(x => x.Id == id).SingleOrDefault();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                    }
                }
            }
            return product;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            List<Product> products = new List<Product>();
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                {
                    try
                    {
                        products = session.QueryOver<Product>()
                            .JoinQueryOver(c => c.Category)
                            .Where(x => x.Id == categoryId)
                            .List<Product>().ToList();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    { 
                        transaction.Rollback();
                    }
                }
            }
            return products;
        }

        public void ChangeQuantityOfProducts(Order order)
        {
            using (var session = NHibernateBase.Session) //tworzymy obiekt połączenie do bazy danych
            {
                foreach (var item in order.ProductOrders)
                {
                    using (var transaction = session.BeginTransaction()) //tworzymy obiekt tranzakcji 
                    {
                        try
                        {
                            item.Product.Quantity -= item.Quantity;
                            session.Update(item.Product);
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                        }
                    }
                }
            }
        }
    }
}
