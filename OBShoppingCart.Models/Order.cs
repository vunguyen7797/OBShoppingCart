namespace OBShoppingCart.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}