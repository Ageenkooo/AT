using Microsoft.EntityFrameworkCore;
using Quartz;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit
{
    public class RabbitSender : IJob
    {
        private RabbitContext _context;
    public RabbitSender()
    {
        string conn = "Data Source=DESKTOP-OLTRDJG;Initial Catalog=Rabbit;User Id=rabbit;Password=loki;";
        var options = new DbContextOptionsBuilder<RabbitContext>();
        options.UseSqlServer(conn);
        _context = new RabbitContext(options.Options);

    }

    public async Task Execute(IJobExecutionContext context)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "RabbitQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            int rand = new Random().Next(1, 8);
            string message =
                _context.Animals.SingleOrDefault(x => x.Id == rand).AnimalName;
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "RabbitQueue", basicProperties: null, body: body);
        }
    }
}
}
