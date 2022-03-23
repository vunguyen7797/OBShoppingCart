using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Services.IServices
{
    public interface IOrderService
    {
        void CreateOrder(OrderCreateDto orderCreateDto);
        void UpdateOrder(Order order);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<OrderViewFullDto>> GetAllOrdersAsync();
        Task<OrderViewFullDto> GetOrderDetailFullAsync(int id);
        Task<OrderViewBriefDto> GetOrderDetailBriefAsync(int id);
        Task<Order> GetSingleOrderAsync(int id);
        Task DeleteOrderAsync(int id);
    }
}
