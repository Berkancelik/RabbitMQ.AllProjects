using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Hosting;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumer;

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args).
    ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMEssageConsumer>();

            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");
                
                _configurator.ReceiveEndpoint("example-message-queue",e =>e.ConfigureConsumer<ExampleMEssageConsumer>(context));
            });
        });

    }).Build();
host.Run();