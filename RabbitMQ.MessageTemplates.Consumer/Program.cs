//Consumer 

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");



using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P (Point-To-Point) Design
//string queueName = "example-p2p-queue";

//channel.QueueDeclare(queue: queueName,
//    durable:false,
//    exclusive:false,
//    autoDelete:false);

//byte[] message = Encoding.UTF8.GetBytes("Hello");
//channel.BasicPublish(exchange: string.Empty,
//    routingKey: queueName,
//    body: message);
#endregion

#region (Publis-Subscribe) Design

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//string queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(
//    queue: queueName,
//    exchange: queueName,
//    routingKey: string.Empty);

//EventingBasicConsumer consumer = new (channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region (Work Queue Design)

//string queueName = "example-work-queue";

//channel.QueueDeclare(queue: queueName,
//    durable: false, exclusive: false, autoDelete: false);

//EventingBasicConsumer consumer = new (channel);

//channel.BasicConsume(queue:queueName,
//    autoAck:true,
//    consumer:consumer);

//channel.BasicQos(
//    prefetchCount: 1,
//    prefetchSize: 0,
//    global: false);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion


#region Request/Response Deisgn​

string requestQueueName = "example-request-response-queue";
channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: requestQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    //.....
    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem tamamlandı. : {message}");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = e.BasicProperties.CorrelationId;
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: e.BasicProperties.ReplyTo,
        basicProperties: properties,
        body: responseMessage);
};

#endregion


Console.Read();


