//Publisher 

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");



using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Litfen header value'sunu giriniz: ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>
    {
        ["no"] = value
    };

    channel.BasicPublish(
        "header-exchange-examplke",
    routingKey: string.Empty,
    body: message,
    basicProperties: basicProperties
        );
}


Console.Read();
