using Microsoft.AspNetCore.Http.HttpResults;
using SOLID.Principles.Demo.Models;

namespace SOLID.Principles.Demo.Services
{
    #region Interface sagregation
    public interface IEmailSender
    {
        void SendEmail(string to, string subject, string body);
    }

    public interface ISmsSender
    {
        void SendSms(string phoneNumber, string message);
    }

    public class EmailSender : IEmailSender
    {
        public void SendEmail(string to, string subject, string body) =>
            Console.WriteLine($"[EMAIL] To: {to} | Subject: {subject} | Body: {body}");
    }


    public class SmsSender : ISmsSender
    {
        public void SendSms(string phoneNumber, string message) =>
            Console.WriteLine($"[SMS] To: {phoneNumber} | {message}");
    }


    public class FullNotificationService : IEmailSender, ISmsSender
    {
        public void SendEmail(string to, string subject, string body) =>
            Console.WriteLine($"[FULL-EMAIL] To: {to} | {subject}");

        public void SendSms(string phoneNumber, string message) =>
            Console.WriteLine($"[FULL-SMS] To: {phoneNumber} | {message}");
    }

    #endregion

    public interface IInterfaceSegregationService
    {
        List<string> Notify(Customer customer);
    }


    public class InterfaceSegregationService : IInterfaceSegregationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        public InterfaceSegregationService(IEmailSender emailSender, ISmsSender smsSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public List<string> Notify(Customer customer)
        {
           var results = new List<string>();

            foreach (var channel in customer.NotifyBy)
            {
                switch (channel)
                {
                    case "Email":
                        _emailSender.SendEmail(customer.CustomerEmail, "Test Interface Sagregation", "Test Email Body");
                        results.Add("Email sent");
                        break;
                    case "Sms":
                        _smsSender.SendSms(customer.CustomerPhoneNumber, "Test Interface Sagregation");
                        results.Add("SMS sent");
                        break;
                }
            }

            return results;
        }
    }
}
