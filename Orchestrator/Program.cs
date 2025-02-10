using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orchestrator;
using SharedContracts.Events;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMassTransit(x =>
//{
//    x.SetKebabCaseEndpointNameFormatter();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("rabbitmq://localhost");
//        cfg.ConfigureEndpoints(context);
//    });

//    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
//     .InMemoryRepository();
//});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .InMemoryRepository();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Orchestrator Service is Running...");
app.Run();
