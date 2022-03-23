using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.DataAccess.Dtos
{
    public class OrderPartialUpdateDto
    {
        public string CustomerId { get; set; }
        [MaxLength(256)]
        public string CustomerName { get; set; }
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Phone]
        public string CustomerPhone { get; set; }
        [MaxLength(256)]
        public string ShippingAddress { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public virtual IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
