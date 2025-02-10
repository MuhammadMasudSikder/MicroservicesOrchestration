using MassTransit;
using Orchestrator;
using PaymentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// Register MassTransit
//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<OrderPlacedConsumer>();
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("rabbitmq://localhost"); // Update with actual RabbitMQ host
//    });
//});

//var saga = new OrderStateMachine();
//var repo = new OrderState();

//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<OrderPlacedConsumer>();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("rabbitmq://localhost"); // Update with correct host if needed
//        cfg.ReceiveEndpoint("order-placed-queue", e =>
//        {
//            e.ConfigureConsumer<OrderPlacedConsumer>(context);
//            e.StateMachineSaga(saga, repo);
//        });
//    });
//});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProcessPaymentConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("payment-queue", e =>
        {
            e.ConfigureConsumer<ProcessPaymentConsumer>(context);
        });
    });
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();