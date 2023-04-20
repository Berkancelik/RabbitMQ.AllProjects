using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.ReequestResponseMessage;

string rabbitMQUri = "amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf";

string request = "request-queue";



IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
  
});

 bus.CreateRequestClient < RequestMessage>(new Uri($"{rabbitMQUri}/{request}"));

bus.StartAsync();

Console.Read();


