using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedContracts.Events
{
    public interface ICreateOrder
    {
        Guid OrderId { get; set; }
        string Product { get; set; }
        decimal Price { get; set; }
    }


    public interface IPaymentSucceeded
    {
        Guid OrderId { get; set; }
    }

    public interface IPaymentFailed
    {
        Guid OrderId { get; set; }
        public string Reason { get; set; }
    }

    public interface IOrderCompleted
    {
        Guid OrderId { get; }
    }

    public interface IOrderFailed
    {
        Guid OrderId { get; }
        string Reason { get; }
    }

    public interface IProcessPayment
    {
        Guid OrderId { get; }
        decimal Amount { get; }
    }

    public class ProcessPayment : IProcessPayment
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }

    public class OrderCompleted : IOrderCompleted
    {
        public Guid OrderId { get; set; }
    }

    public class OrderFailed : IOrderFailed
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; }
    }

}
