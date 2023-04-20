using Microsoft.Extensions.Hosting;

using MassTransit.Transports;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services;

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(configurator =>
    {
        configurator.UsingRabbitMq((context, _configurator) =>
        {
            _configurator.Host("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");
        });
    });
    services.AddHostedService<PublishMessageService>(provider =>
    {
        using IServiceScope scope = provider.CreateScope();
        IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
        return new(publishEndpoint);
    });
}).Build();

await host.RunAsync();