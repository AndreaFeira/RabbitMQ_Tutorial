using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Producer
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

                //The connection abstracts the socket connection, and takes care of protocol version negotiation and authentication and so on...
                using (IConnection connection = factory.CreateConnection())
                {
                    if (!connection.IsOpen) throw new ApplicationException("Connection is close.");

                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello_queue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        Console.WriteLine("Press [enter] to proceed.");
                        Console.ReadLine();

                        string message = "Hello World!";

                        byte[] body = Encoding.UTF8.GetBytes(message);

                        Console.ReadLine();

                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);

                        Console.WriteLine("Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }

}
