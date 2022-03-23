using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Utility.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderViewFullDto>();
            CreateMap<Order, OrderViewBriefDto>();
            CreateMap<OrderUpdateDto, Order>();

            CreateMap<JsonPatchDocument<OrderPartialUpdateDto>, JsonPatchDocument<Order>>();
            CreateMap<Operation<OrderPartialUpdateDto>, Operation<Order>>();

            CreateMap<Order, OrderPartialUpdateDto>().ReverseMap();
            CreateMap<OrderPartialUpdateDto, Order>();
        }
    }
}
