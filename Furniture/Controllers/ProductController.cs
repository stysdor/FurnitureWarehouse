using AutoMapper;
using Core.Domain;
using Core.Repositories;
using Core.Repositories.Interface;
using Furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        private IMapper _mapper;

        // GET: Product
        public ProductController(IMapper mapper)
        {
            _repository = new ProductRepository();
            _mapper = mapper;
        }

        public ActionResult Index()
        {

            var list = _repository.GetProducts();
            if (list == null)
                list = new List<Product>();
            return View(_mapper.Map<List<ProductModel>>(list));
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductModel model = new ProductModel()
            {
                Categories = GetCategories(),
                Colors = GetColors()
            };
            return View(model);
        }

        private List<CategoryModel> GetCategories()
        {
            var list = _repository.GetCategories();
            return _mapper.Map<List<CategoryModel>>(list);
        }
        private List<ColorModel> GetColors()
        {
            var list = _repository.GetColors();
            return _mapper.Map<List<ColorModel>>(list);
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                if (model.Id == 0)
                {
                   result = _repository.AddProduct(_mapper.Map<Product>(model));
                }
                else
                {
                    result = _repository.EditProduct(_mapper.Map<Product>(model));
                }
                if (result)
                    return RedirectToAction("Index");
            }
            model.Categories = (List<CategoryModel>)TempData["Categories"];
            model.Colors = (List<ColorModel>)TempData["Colors"];
            return View("AddProduct", model);
        }

        public ActionResult EditProduct(ProductModel model)
        {
            model = _mapper.Map<ProductModel>(_repository.GetProduct(model.Id));
            model.Categories = GetCategories();
            model.Colors = GetColors();
            return View(model);
        }

        public ActionResult DeleteProduct(ProductModel model)
        {
            var result = _repository.DeleteProduct(_mapper.Map<Product>(model));
            return RedirectToAction("Index");
        }


    }
}