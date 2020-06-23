using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Interface
{
    public interface IProductRepository
    {
        List<Category> GetCategories();

        List<Color> GetColors();

        List<Product> GetProducts();

        bool AddProduct(Product product);
        bool DeleteProduct(Product product);
        bool EditProduct(Product product);
        Product GetProduct(int id);

        void ChangeQuantityOfProducts(Order order);
    }
}
