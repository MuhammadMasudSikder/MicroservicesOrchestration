using MassTransit;
using SharedContracts.Events;

namespace ShippingService
{
    //// Shipping Scheduled Consumer
    //public class ShippingScheduledConsumer : IConsumer<ShippingScheduled>
    //{
    //    private readonly IPublishEndpoint _publishEndpoint;
    //    public ShippingScheduledConsumer(IPublishEndpoint publishEndpoint)
    //    {
    //        _publishEndpoint = publishEndpoint;
    //    }
    //    public async Task Consume(ConsumeContext<ShippingScheduled> context)
    //    {
    //        Console.WriteLine($"Shipping Scheduled: {context.Message.OrderId}");
    //        //await _publishEndpoint.Publish(new OrderCompleted { OrderId = context.Message.OrderId });
    //    }
    //}
}
