using AutoMapper;
using Core.Repositories.Interface;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain;
using Furniture.Models;

namespace Furniture.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        // GET: Product
        public OrderController(IMapper mapper)
        {
            _repository = new OrderRepository();
            _productRepository = new ProductRepository();
            _mapper = mapper;
        }

        // GET: Order
        public ActionResult Index()
        {
            var list = _repository.GetOrders();
            if (list == null)
                list = new List<Order>();
            return View(_mapper.Map<List<OrderModel>>(list));
        }

        public PartialViewResult ProductOrder()
        {
            var model = new ProductOrderModel();
            model.Products = _mapper.Map<List<ProductModel>>(_productRepository.GetProducts());
            return PartialView("ProductOrder", model);
        }


        [HttpGet]
        public ActionResult AddOrder()
        {
            OrderModel model = new OrderModel();
            model.Products = _mapper.Map<List<ProductModel>>(_productRepository.GetProducts());
            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderModel model)
        {
            if (model.ProductOrders != null)
            {
                foreach (var item in model.ProductOrders)
                {
                    if (item.Product != null && item.Product.Id > 0)
                    {
                        item.Product = _mapper.Map<ProductModel>(_productRepository.GetProduct(item.Product.Id));
                        if (item.Quantity > item.Product.Quantity)
                        {
                            ModelState.AddModelError("Quantity", "Maksymalna ilość zamówienia to " + item.Product.Quantity);
                            return View(model);
                        }
                        item.Order = model;    
                        model.Amount = model.Amount + (item.Quantity * item.Product.Price);
                    }
                    item.Products = (List<ProductModel>)TempData["Products"];
                }
            }
            else return View(model);

            bool result = false;
            model.OrderDate = DateTime.Now;
            result = _repository.AddOrder(_mapper.Map<Order>(model));

            if (result)
            {
                changeProductsQuantity(model);
                TempData["Succeed"] = model.Amount;
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private void changeProductsQuantity(OrderModel model)
        {
            _productRepository.ChangeQuantityOfProducts(_mapper.Map<Order>(model));
        }
    }
}