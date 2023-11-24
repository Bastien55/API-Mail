using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CoreLibrary;
using System.Collections;

namespace API_Mail.RabbitMQConsumer
{
    public class ConsumerMQ
    {
        public ConsumerMQ(string hostname, string queue)
        {
            try
            {
                var channel = RabbitMQManager.Instance.CreateChannel(hostname, queue);

                Console.WriteLine(" [x] Waiting Message...");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += Consumer_Received;

                channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($" [x] Received Message {message}");

            MailManager.MailManager.Instance.ReceiveData(message);
        }
    }
}
