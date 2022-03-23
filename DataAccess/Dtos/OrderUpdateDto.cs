using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.DataAccess.Dtos
{
    public class OrderUpdateDto
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }
        [Required]
        [MaxLength(256)]
        public string ShippingAddress { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        [Required]
        public virtual IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
