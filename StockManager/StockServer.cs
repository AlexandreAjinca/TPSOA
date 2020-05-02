using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace StockManager
{
    class StockServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stock server");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "stock_queue", autoAck: false, consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine(" [.] Checking item:{0}", message);
                        response = getProduct(message);

                        //response = getAllProducts();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" [.] " + e.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine(" [.] Response sent to client");
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public static string getProduct(string itemName)
        {
            StreamReader file = new StreamReader("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", true);
            String json = file.ReadToEnd();
            var obj = JObject.Parse(json);
            //On parcours le JSON pour récupérer la ligne de l'item
            foreach (JObject element in obj["product"])
            {
                string n = element["nom"].ToString();
                if ( n==itemName)
                {
                    file.Close();
                    return element.ToString();
                }
            }
            file.Close();
            return "null";
        }

        /// <summary>
        /// Assumes only valid positive integer input.
        /// Don't expect this one to work for big numbers, and it's probably the slowest recursive implementation possible.
        /// </summary>
        public static string getAllProducts()
        {
            StreamReader file = new StreamReader("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", true);
            String json = file.ReadToEnd();
            string recup = " NOS PRODUITS ";
            var obj = JObject.Parse(json);
            foreach (JObject element in obj["product"])
            {
                recup = recup + "\n" + element["nom"] + "  " + element["prix"] ;
            }
            return recup;
        }
    }
}
