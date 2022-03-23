using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.DataAccess.Dtos
{
    public class OrderViewFullDto : OrderViewBriefDto
    {
        public virtual IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
