using RabbitMQ.Client;
using System.Text;



// Connection Created
ConnectionFactory factory = new();

factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");


// Connection activeted and channel opened

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queuo Created

channel.QueueDeclare(queue: "example-queue", exclusive:false, durable:true);


//Queue message send 

//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir.. 

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
//Console.Read();

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties:properties);
    Console.Read();

}