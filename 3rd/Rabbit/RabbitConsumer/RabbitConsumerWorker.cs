using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitConsumer
{
    public class ConsumeRabbitMQHostedService  : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;

        public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
            InitRabbitMQ();
        }

	private static string SendDataToAzure(string data)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://rabbitfunction20191218083036.azurewebsites.net/api/RabbitFunction?name={data}");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("RabbitQueue", false, false, false, null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);

                HandleMessage(content);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("RabbitQueue", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        { 
            _logger.LogInformation($"consumer received {content}");
            var azureReply = SendDataToAzure(content);
            SendEmail(azureReply);
            
        }

        private static void SendEmail(string mes)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Loki",
                "loki@god.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("Irina",
                "ira.ageenkoo@gmail.com");
            message.To.Add(to);

            message.Subject = "Email subject";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<h1>Azure message: {mes}</h1>";
            bodyBuilder.TextBody = mes;

            message.Body = bodyBuilder.ToMessageBody();
            Console.WriteLine(message.Body);
            SmtpClient client = new SmtpClient();

            Console.WriteLine("Client initialized");
            try
            {
		client.Connect("smtp.gmail.com", Convert.ToInt32(465), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Client connected");
            try
            {
                client.Authenticate("ira.ageenkoo@gmail.com", "hf48vp56");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("client");
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }


        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}