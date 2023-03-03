using RabbitMQ.Client;
using System.Text;



// Connection Created
ConnectionFactory factory = new();

factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");


// Connection activeted and channel opened

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queuo Created

channel.QueueDeclare(queue: "example-queue", exclusive:false);


//Queue message send 

//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir.. 

byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
Console.Read();