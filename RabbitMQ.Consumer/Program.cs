//Consumer 

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


factory.Uri = new("amqps://tfnsxluf:Q7118mxTG5u1eYr4qIUyUtwmG2C8PAm5@woodpecker.rmq.cloudamqp.com/tfnsxluf");



using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
//1
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
//2
string queueName =channel.QueueDeclare().QueueName;
//3
channel.QueueBind(queue: queueName, exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message); ;
};

Console.Read();


//1 Publisher daki aynı kuyruk ve type a sahip olması gerekir 

//2. adım:  publisher tarafından routing key'de bulunan değerdeki kuyruğa gönderileb mesajları endi oluşturduğumuz kuyruğa yönlendirmemiz gerekir. Bunun için önceliikler bir kuyruk oluşturmamız gerekmektedir.