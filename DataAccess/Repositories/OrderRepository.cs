using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OBShoppingCart.Data;
using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.DataAccess.Repositories.IRepositories;
using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void CreateOrder(OrderCreateDto orderCreateDto)
        {
            _db.Orders.Add(_mapper.Map<OrderCreateDto, Order>(orderCreateDto));
        }

        public async Task DeleteOrderAsync(int id)
        {
            Order removeOrder = await _db.Orders.SingleOrDefaultAsync(x => x.Id == id);
            _db.Orders.Remove(removeOrder);
        }

        public async Task<IEnumerable<OrderViewFullDto>> GetAllOrdersAsync()
        {
            return await _db.Orders
                .ProjectTo<OrderViewFullDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<OrderViewBriefDto> GetOrderDetailBriefAsync(int id)
        {
            return await _db.Orders
                .Where(x => x.Id == id)
                .ProjectTo<OrderViewBriefDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<OrderViewFullDto> GetOrderDetailFullAsync(int id)
        {
            return await _db.Orders
                .Where(x => x.Id == id)
                .ProjectTo<OrderViewFullDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<Order> GetSingleOrderAsync(int id)
        {
            return await _db.Orders
                .Include(x => x.OrderItems)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public void UpdateOrder(Order order)
        {
            _db.Orders.Update(order);
        }

    }
}
