using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.DataAccess.Repositories.IRepositories;
using OBShoppingCart.Models;
using OBShoppingCart.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void CreateOrder(OrderCreateDto orderCreateDto) => _orderRepository.CreateOrder(orderCreateDto);

        public async Task DeleteOrderAsync(int id) => await _orderRepository.DeleteOrderAsync(id);  

        public async Task<IEnumerable<OrderViewFullDto>> GetAllOrdersAsync() => await _orderRepository.GetAllOrdersAsync();

        public async Task<OrderViewBriefDto> GetOrderDetailBriefAsync(int id) => await _orderRepository.GetOrderDetailBriefAsync(id);

        public async Task<OrderViewFullDto> GetOrderDetailFullAsync(int id) => await _orderRepository.GetOrderDetailFullAsync(id);

        public async Task<Order> GetSingleOrderAsync(int id) => await _orderRepository.GetSingleOrderAsync(id);

        public async Task<bool> SaveChangesAsync() => await _orderRepository.SaveChangesAsync();

        public void UpdateOrder(Order order) => _orderRepository.UpdateOrder(order);
    }
}
