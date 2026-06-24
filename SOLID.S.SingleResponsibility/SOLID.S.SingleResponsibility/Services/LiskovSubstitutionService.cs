using SOLID.Principles.Demo.Models;

namespace SOLID.Principles.Demo.Services
{
    #region Payment Options

    public interface IPaymentProcess
    {
        string paymentMethod { get; }
        OrderResult processPayment(Order order);
    }

    public interface IRefundProcess : IPaymentProcess
    {
        OrderResult Refund(Guid transactionId);
    }


    public class CreditCardProcessor : IRefundProcess
    {
        public string paymentMethod => "CreditCard";

        public OrderResult processPayment(Order order)
        {
            // implement payment 
            return new OrderResult { Message = OrderMessages.Success, Success = true, TransactionId = new Guid() };
        }

        public OrderResult Refund(Guid transactionId)
        {
            return new OrderResult { Message = OrderMessages.Refund, Success = true, TransactionId = transactionId };
        }
    }

    public class BankTransferProcessor : IRefundProcess
    {
        public string paymentMethod => "BankTransfer";

        public OrderResult processPayment(Order order)
        {
            // implement payment 
            return new OrderResult { Message = OrderMessages.Success, Success = true, TransactionId = new Guid() };
        }

        public OrderResult Refund(Guid transactionId)
        {
            return new OrderResult { Message = OrderMessages.Failed, Success = false, TransactionId = transactionId };
        }
    }


    #endregion


    public interface ILiskovSubstitutionService
    {
        OrderResult processPayment(Order order);
        OrderResult RefundPayment(string paymentMethod, Guid transactionId);
    }

    public class LiskovSubstitutionService: ILiskovSubstitutionService
    {
        private readonly IEnumerable<IPaymentProcess> _paymentProcess;

        public LiskovSubstitutionService(IEnumerable<IPaymentProcess> paymentProcess)
        {
            _paymentProcess = paymentProcess;
        }

        // Callers can always substitute any IPaymentProcessor without surprises
        public OrderResult processPayment(Order order)
        {
            var selectedPaymentProcess = _paymentProcess.FirstOrDefault(payment => payment.paymentMethod == order.PaymentMethod);

            if(selectedPaymentProcess == null) {
                return new OrderResult() { Message = OrderMessages.Failed, Success = false };
            }

            return selectedPaymentProcess.processPayment(order);
        }


        public OrderResult RefundPayment(string paymentMethod, Guid transactionId)
        {
            var selectedPaymentProcess = _paymentProcess.FirstOrDefault(payment => payment.paymentMethod == paymentMethod);

            //this should allow as per Principle
            if (selectedPaymentProcess is IRefundProcess refundable)
            {
                return refundable.Refund(transactionId);
            }

            return new OrderResult() { Message = OrderMessages.Failed, Success = false };
        }

    }
}
