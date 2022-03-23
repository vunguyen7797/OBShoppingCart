using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Models
{
    public class OrderItem
    {
        [Key]
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order ParentOrder { get; set; }
    }
}
