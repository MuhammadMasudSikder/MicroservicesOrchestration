using MassTransit;
using SharedContracts.Events;

namespace InventoryService
{
    //// Stock Reserved Consumer
    //public class StockReservedConsumer : IConsumer<StockReserved>
    //{
    //    private readonly IPublishEndpoint _publishEndpoint;
    //    public StockReservedConsumer(IPublishEndpoint publishEndpoint)
    //    {
    //        _publishEndpoint = publishEndpoint;
    //    }
    //    public async Task Consume(ConsumeContext<StockReserved> context)
    //    {
    //        Console.WriteLine($"Stock Reserved: {context.Message.OrderId}");
    //        await _publishEndpoint.Publish(new ShippingScheduled (context.Message.OrderId));
    //    }
    //}

}
