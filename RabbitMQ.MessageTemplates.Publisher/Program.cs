//Publisher 

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
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue:queueName ,
//    autoAck:false,
//    consumer: consumer);

//consumer.Received += (sernder, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//}; 
#endregion

#region (Publis-Subscribe) Design

//string exchangeName = "example-pub-sub-exchange";
//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(1000); 

//    byte[] message = Encoding.UTF8.GetBytes("merhaba"+i);

//    channel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);
//}


#endregion

#region (Workı Queue Design)

string queueName = "example-work-queue";

channel.QueueDeclare(queue: queueName,
    durable: false, exclusive: false, autoDelete: false);
 
for (int i = 0; i < 100; i++)
{
    await Task.Delay(1000);

    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

    channel.BasicPublish(
        exchange: queueName,
        routingKey: string.Empty,
        body: message);
}


#endregion




Console.Read();
