using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class ProductOrderModel
    {

        public int Id { get; set; }
        public OrderModel Order { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Produkt")]
        public ProductModel Product { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Range(1,50, ErrorMessage ="Minimalna ilość to 1, a maksymalna 50 szt.")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }


        public List<ProductModel> Products { get; set; }
    }
}