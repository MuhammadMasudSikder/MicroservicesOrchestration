using MassTransit;
using OrderService;

var builder = WebApplication.CreateBuilder(args);

// Register MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderFailedConsumer>(); // Register the consumer
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Update with actual RabbitMQ host
        cfg.ReceiveEndpoint("order-failed-queue", e =>
        {
            e.ConfigureConsumer<OrderFailedConsumer>(context);
        });
    });
});
//builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

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
