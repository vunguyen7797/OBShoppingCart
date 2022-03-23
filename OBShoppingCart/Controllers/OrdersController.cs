using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OBShoppingCart.Data;
using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.Models;
using OBShoppingCart.Services.IServices;
using OBShoppingCart.Utility.LoggerService;

namespace OBShoppingCart.Controllers
{
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public OrdersController(IOrderService orderService, IMapper mapper, ILoggerManager logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create a new order with items on the order.
        /// </summary>
        /// <param name="orderCreateDto">Order payload</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateOrder(OrderCreateDto orderCreateDto)
        {
            if (!orderCreateDto.OrderItems.Any())
            {
                return BadRequest("The order cannot be created with zero item.");
            }

            _orderService.CreateOrder(orderCreateDto);

            if (await _orderService.SaveChangesAsync()) return Ok("Order has been created.");

            return BadRequest("Failed to create new order.");
        }

        /// <summary>
        /// Update entire order if any changes
        /// </summary>
        /// <param name="orderUpdateDto">Update order payload</param>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateOrder([FromBody]OrderUpdateDto orderUpdateDto, int id)
        {
            Order order = await _orderService.GetSingleOrderAsync(id);

            _mapper.Map(orderUpdateDto, order);

            _orderService.UpdateOrder(order);

            if (await _orderService.SaveChangesAsync()) return NoContent();

            return BadRequest("Failed to update the order.");
        }

        /// <summary>
        /// Update an order partially
        /// </summary>
        /// <param name="orderPartialUpdateDto">Patch document</param>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePartialOrder([FromBody]JsonPatchDocument<OrderPartialUpdateDto> orderPartialUpdateDto, int id)
        {
            Order order = await _orderService.GetSingleOrderAsync(id);

            if (order == null) return NotFound();

            OrderPartialUpdateDto mappedOrderDto = _mapper.Map<OrderPartialUpdateDto>(order);

            orderPartialUpdateDto.ApplyTo(mappedOrderDto, ModelState);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order updatedOrder = _mapper.Map(mappedOrderDto, order);

            _orderService.UpdateOrder(updatedOrder);

            if (await _orderService.SaveChangesAsync()) return NoContent();

            return BadRequest("Failed to update the order.");
        }

        /// <summary>
        /// Get all created orders.
        /// </summary>
        /// <returns>List of full detail orders.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderViewFullDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return orders == null ? NotFound() : Ok(orders);
        }

        /// <summary>
        /// Get a full detail of an order
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderViewFullDto>> GetOrderDetailFull(int id)
        {
            var order = await _orderService.GetOrderDetailFullAsync(id);

            return order == null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Get a brief order without item details.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpGet("short/{id}")]
        [Authorize]
        public async Task<ActionResult<OrderViewBriefDto>> GetOrderDetailBrief(int id)
        {
            var order = await _orderService.GetOrderDetailBriefAsync(id);

            return order == null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Completely delete a single order.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);

            if (await _orderService.SaveChangesAsync()) return NoContent();

            return BadRequest("Failed to delete the order.");
        }
    }
}
