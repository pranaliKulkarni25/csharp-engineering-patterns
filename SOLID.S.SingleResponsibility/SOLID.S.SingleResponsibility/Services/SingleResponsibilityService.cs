

using SOLID.Principles.Demo.Models;

namespace SOLID.S.SingleResponsibility.Service
{
    public interface IOrderService
    {
        string ProcessOrder(Order order);
    }

    public interface IEmailService
    {
        void SendOrderConfirmation(Order order);
    }

    public interface IOrderRepository
    {
        void SaveOrder(Order order);
    }

    public interface IInvoiceService
    {
        string GenerateInvoice(Order order);
    }

    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IEmailService emailService, IInvoiceService invoiceService, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _invoiceService = invoiceService;
            _logger = logger;
        }

        // Responsibility is to only process the order, not to save, send email or generate invoice
        public string ProcessOrder(Order order)
        {
            if (order.Items == null || order.Items.Count == 0)
                return "Order has no items";

            // calculate totals and update status
            order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);
            order.Status = OrderStatus.Confirmed;

            _orderRepository.SaveOrder(order);
            _emailService.SendOrderConfirmation(order);
            var invoice = _invoiceService.GenerateInvoice(order);

            _logger.LogInformation("Order {OrderId} processed. Invoice: {Invoice}", order.Id, invoice);

            return $"Order {order.Id} processed successfully";
        }

    }

    // Responsibility to only Save order into DB
    public class OrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {

            // Code to save the order to the database
            Console.WriteLine("Order saved to the database.");
        }
    }

    // Responsibility to only send emails
    public class EmailService : IEmailService
    {
        public void SendOrderConfirmation(Order order)
        {
            // sending email logic (e.g., using SMTP client or an email API)
            Console.WriteLine($"[EMAIL] Confirmation sent to {order.CustomerEmail} for Order #{order.Id}");
        }
    }

    // Responsibility is to only generate invoice
    public class InvoiceService : IInvoiceService
    {
        public string GenerateInvoice(Order order)
        {
            // PDF / HTML invoice logic here — nothing else
            var invoice = $"INV-{order.Id:D6} | {order.CustomerName} | {order.TotalAmount:C}";
            Console.WriteLine($"[INVOICE] Generated: {invoice}");
            return invoice;
        }
    }
}
