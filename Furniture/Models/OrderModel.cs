using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            ProductOrders = new List<ProductOrderModel>();
        }

        public int Id { get; set; }

        [Display(Name = "Suma zamówienia")]
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }

        [Display(Name = "Produkty")]
        public IList<ProductOrderModel> ProductOrders { get; set; }


        public List<ProductModel> Products { get; set; }
    }
}