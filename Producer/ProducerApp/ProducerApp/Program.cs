using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
namespace ProducerApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Bağlantı (duruma göre ip)
            var factory = new ConnectionFactory() { HostName = "localhost"};


            using(var connection = factory.CreateConnection())
            {
                // mesaj kanalı
                using (var channel = connection.CreateModel())
                {
                    bool close = false;
                    while (!close)
                    {
                        channel.QueueDeclare("FirstQueue",
                      durable: false,
                      exclusive: false,
                      autoDelete: true,
                      arguments: null
                      );

                        string message = JsonConvert.SerializeObject(new Message { Content = "İlk rabbit testi", Title = "Rabbitmq test başlık" });

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "", routingKey: "FirstQueue", basicProperties: null, body: body);

                        Console.WriteLine("[x] Sent {0}", message);

                        string result = Console.ReadLine();
                        close = bool.Parse(result);
                    }
                }
            }







            Console.ReadKey();
        }
    }

    class Message
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
