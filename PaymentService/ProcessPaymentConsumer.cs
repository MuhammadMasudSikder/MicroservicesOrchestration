using MassTransit;
using SharedContracts.Events;

namespace PaymentService
{
    public class ProcessPaymentConsumer : IConsumer<IProcessPayment>
    {
        private readonly IBus _bus;

        public ProcessPaymentConsumer(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<IProcessPayment> context)
        {
            var success = new Random().Next(1, 10) > 3; // 70% success rate

            if (success)
            {
                Console.WriteLine($"Payment Succeeded: {context.Message.OrderId}");
                await _bus.Publish<IPaymentSucceeded>(new
                {
                    OrderId = context.Message.OrderId
                });

            }
            else
            {
                Console.WriteLine($"Payment Failed: {context.Message.OrderId}");
                await _bus.Publish<IPaymentFailed>(new
                {
                    OrderId = context.Message.OrderId
                });
            }
        }
    }
}
