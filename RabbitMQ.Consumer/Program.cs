//Connection createsd

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");


// Connection activeted and channel opened

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queue Created

channel.QueueDeclare(queue: "example-queue", exclusive: false, durable:false); // Consumer Queue  == Publicher Queue
EventingBasicConsumer consumer = new(channel);

//autoAck mesaj kuruktan alındıktan sonra silinip silinemesi
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
channel.BasicQos(0, 1, false);

//kuyruğa gelen mesajın işlendiği yer 
consumer.Received += (sender, e) =>
{
    //e.Body :kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.toArray() : Kuyruktaki mesajın byte dönüştürmemiz gerekmektedir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

