using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Category = new CategoryModel();
            Color = new ColorModel();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Nazwa musi się składać minimum z 3 znaków")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kategoria")]
        public CategoryModel Category { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kolor")]
        public ColorModel Color { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Range(1,20000, ErrorMessage ="Cena musi zawierać się w zakresie 1 - 20 000zł")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Range(0, 20000, ErrorMessage = "Ilość musi zawierać się w zakresie 0 - 20 000")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        public List<CategoryModel> Categories;
        public List<ColorModel> Colors;
    }
} 