using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace AnotherSampleProject.API.Services;

public class RabbitMQService(IOptions<RabbitMQSettings> options) : IRabbitMQService
{
    private readonly RabbitMQSettings settings = options.Value;

    public void SendAddTestModelMessage(string name, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = settings.HostName,
            UserName = settings.UserName,
            Password = settings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "TestModel_Notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);

        string message = $"New entity added: {name}";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: "TestModel_Notifications", basicProperties: null, body: body);
        Console.WriteLine($"Add TestModel with name {name} informed...");
    }
}
