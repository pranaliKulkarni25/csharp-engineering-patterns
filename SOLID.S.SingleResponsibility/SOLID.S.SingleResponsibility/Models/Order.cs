namespace SOLID.Principles.Demo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string CustomerType { get; set; } = "Regular"; // For discount strategy
        public decimal DiscountedAmount { get; set; } = 0; // For storing discounted amount
        public string PaymentMethod {  get; set; } = string.Empty;
    }

    public class OrderItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }

    public static class OrderMessages
    {
        public static readonly string Success = "Order Placed Successfully";
        public static readonly string Refund = "Order Refunded Successfully";
        public static readonly string Failed = "Issue Occured";
    }


    public class OrderResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid TransactionId { get; set; }
    }
}
