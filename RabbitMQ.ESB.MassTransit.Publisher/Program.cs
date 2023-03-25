using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMQUri = "amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf";


string queueName = "example-queue";

// Console olduğu için Bus

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek mesaj :");
string message = Console.ReadLine();
sendEndpoint.Send<IMessage>(new ExampleMessage()
{
        Text = message
});

Console.Read();

 