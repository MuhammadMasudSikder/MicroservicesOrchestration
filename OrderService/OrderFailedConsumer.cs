using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedContracts.Events;

namespace OrderService
{
    // Order Placed Consumer
    public class OrderFailedConsumer : IConsumer<IOrderFailed>
    {
        private readonly IBus _bus;

        public OrderFailedConsumer(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<IOrderFailed> context)
        {
            Console.WriteLine($"Order Failed: {context.Message.OrderId}");
            //Rollback/Delete the transaction
            //await _bus.Publish(new PaymentProcessed(context.Message.OrderId));
        }
    }

}
