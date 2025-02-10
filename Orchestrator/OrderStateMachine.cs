using MassTransit;
using SharedContracts.Events;
using System;

namespace Orchestrator
{
    /*
     * Event Flow Summary
        ✅ OrderController → Publishes OrderPlaced
        ✅ PaymentController → Listens for OrderPlaced, processes payment, and Publishes PaymentProcessed
        ✅ InventoryController → Listens for PaymentProcessed, reserves stock, and Publishes StockReserved
        ✅ ShippingController → Listens for StockReserved, schedules shipping, and Publishes ShippingScheduled

        How Our Saga Will Work
            1️⃣ OrderService → Publishes OrderPlaced
            2️⃣ PaymentService → Listens for OrderPlaced, processes payment, and Publishes PaymentProcessed
            3️⃣ InventoryService → Listens for PaymentProcessed, reserves stock, and Publishes StockReserved
            4️⃣ ShippingService → Listens for StockReserved, schedules shipping, and Publishes ShippingScheduled
            5️⃣ Saga Orchestrator → Listens to all events and ensures workflow completion.

     */

    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State OrderCreated { get; private set; }
        public State PaymentProcessing { get; private set; }
        public State Completed { get; private set; }
        public State Failed { get; private set; }

        public Event<ICreateOrder> OrderPlaced { get; private set; }
        public Event<IPaymentSucceeded> PaymentCompleted { get; private set; }
        public Event<IPaymentFailed> PaymentFailed { get; private set; }
        public Event<IOrderCompleted> OrderCompleted { get; private set; }
        public Event<IOrderFailed> OrderFailed { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderPlaced, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentCompleted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentFailed, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCompleted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderFailed, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderPlaced)
                    .Then(context =>
                    {
                        Console.WriteLine($"Order received: {context.Data.OrderId}");
                    })
                    .TransitionTo(OrderCreated)
                    .Publish(context => new ProcessPayment
                    {
                        OrderId = context.Data.OrderId,
                        Amount = context.Data.Price
                    })
                    );

            During(OrderCreated,
                When(PaymentCompleted)
                     .Then(context =>
                     {
                         Console.WriteLine($"Payment succeeded for order: {context.Data.OrderId}");
                     })
                    .TransitionTo(Completed)
                    //.Publish(context => new OrderCompleted
                    //{
                    //    OrderId = context.Data.OrderId
                    //})
                    ,

                When(PaymentFailed)
                      .Then(context =>
                      {
                          Console.WriteLine($"Payment failed for order: {context.Data.OrderId}, rolling back.");
                      })
                     .Publish(context => new OrderFailed
                     {
                         OrderId = context.Data.OrderId,
                         Reason = context.Data.Reason
                     })
                     .TransitionTo(Failed)
                    );

            // Handle OrderFailed event in Failed state
            During(Failed,
                When(OrderFailed)
                    .Then(context =>
                    {
                        Console.WriteLine($"Order {context.Data.OrderId} has already failed: {context.Data.Reason}");
                    })
                    .Finalize());

            // Handle OrderCompleted in Completed state (Optional)
            During(Completed,
                When(OrderCompleted)
                    .Then(context =>
                    {
                        Console.WriteLine($"Order {context.Data.OrderId} has been completed.");
                    })
                    .Finalize());

            // Handle OrderFailed in the Final state to prevent unhandled event errors
            During(Final,
                When(OrderFailed)
                    .Then(context =>
                    {
                        Console.WriteLine($"Order {context.Data.OrderId} already finalized as failed.");
                    }));
            SetCompletedWhenFinalized();
        }
    }
}
