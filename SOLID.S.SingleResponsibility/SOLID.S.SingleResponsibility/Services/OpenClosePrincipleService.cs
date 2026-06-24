
using SOLID.Principles.Demo.Models;

namespace SOLID.Principles.Demo.Services
{
    public interface IOpenClosePrincipleService
    {
        decimal ApplyDiscount(Order order);
    }

    public interface IDiscountStrategy
    {
        string CustomerType { get; }
        decimal Apply(decimal amount);
    }

    public class SeasonalDiscountStrategy : IDiscountStrategy
    {
        public string CustomerType => "Seasonal";
        public decimal Apply(decimal amount) => amount * 0.90m; // 10% off
    }

    public class LoyaltyDiscount: IDiscountStrategy
    {
        public string CustomerType => "Loyalty";
        public decimal Apply(decimal amount) => amount * 0.85m; // 15% off
    }

    public class NewCustomerDiscount : IDiscountStrategy
    {
        public string CustomerType => "NewCustomer";
        public decimal Apply(decimal amount) => amount * 0.95m; // 5% off
    }

    public class OpenClosePrincipleService : IOpenClosePrincipleService
    {
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        public OpenClosePrincipleService(IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _discountStrategies = discountStrategies;
        }

        public decimal ApplyDiscount(Order order)
        {
            var strategy = _discountStrategies.FirstOrDefault(s => s.CustomerType == order.CustomerType);
            if (strategy is null)
                return order.TotalAmount; // No discount
            var discounted = strategy.Apply(order.TotalAmount);
            order.DiscountedAmount = discounted;
            return discounted;
        }
    }
}
