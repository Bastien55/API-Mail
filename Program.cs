using API_Mail.RabbitMQConsumer;
using Microsoft.AspNetCore.Builder;

namespace API_Mail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            Console.WriteLine(args[2]);
            Console.WriteLine(args[3]);

            Console.WriteLine(int.Parse(args[3]));

            MailManager.MailManager.Instance.SetParam(args[2], int.Parse(args[3]));

            var rabbitmq = new ConsumerMQ(args[0], args[1]);

            var app = builder.Build();

            app.Run();
        }
    }
}