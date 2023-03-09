//Publisher 

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");



using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P'P (Point-To-Point) Design
string queueName = "exampke-p2p-queue";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue:queueName ,
    autoAck:false,
    consumer: consumer);

consumer.Received += (sernder, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
}; 
#endregion




Console.Read();
