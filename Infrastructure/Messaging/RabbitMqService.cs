using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

public class RabbitMqService: IRabbitMqService
{
    private readonly string _hostname;
    private readonly string _queueName;
    private readonly string _username;
    private readonly string _password;

    public RabbitMqService(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMq:Hostname"] ?? "localhost";
        _queueName = configuration["RabbitMq:QueueName"] ?? "default_queue";
        _username = configuration["RabbitMq:Username"] ?? "guest";
        _password = configuration["RabbitMq:Password"] ?? "guest";
    }

    public string GetQueueName() => _queueName;

    public ConnectionFactory GetConnectionFactory()
    {
        return new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _username,
            Password = _password
        };
    }

    public void SendMessage(object message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _username,
            Password = _password
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
