using AutoMapper;
using Core.Domain;
using Furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ColorModel, Color>().ReverseMap();
            cfg.CreateMap<CategoryModel, Category>().ReverseMap();
            cfg.CreateMap<ProductModel, Product>().ReverseMap();
            //cfg.CreateMap<ProductOrderModel, ProductOrder>().ReverseMap();
            cfg.CreateMap<OrderModel, Order>().ReverseMap();
            cfg.CreateMap<ProductOrderModel, ProductOrder>().ReverseMap();

        }).CreateMapper();
    }
}