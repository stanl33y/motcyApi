using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class MotorcycleYearNotificationConsumer : BackgroundService
{
    private readonly RabbitMqService _rabbitMqService;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MotorcycleYearNotificationConsumer> _logger;
    private IConnection _connection;
    private IModel _channel;

    public MotorcycleYearNotificationConsumer(
        RabbitMqService rabbitMqService
        , IServiceScopeFactory scopeFactory
        , ILogger<MotorcycleYearNotificationConsumer> logger
    ) {
        _rabbitMqService = rabbitMqService;
        _scopeFactory = scopeFactory;
        _logger = logger;

        InitializeRabbitMQListener();
    }

    private void InitializeRabbitMQListener()
    {
        var factory = _rabbitMqService.GetConnectionFactory();
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _rabbitMqService.QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _logger.LogInformation("RabbitMQ listener initialized");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(body);
            var eventData = JsonConvert.DeserializeObject<dynamic>(jsonMessage);

            if (eventData.Event == "MotorcycleYearNotification")
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    var motorcycle = eventData.Data.ToObject<Motorcycle>();
                    _logger.LogInformation($"Processing notification for motorcycle: {motorcycle.Model}, Year: {motorcycle.Year}");

                    var notification = new Notification
                    {
                        Message = $"Motorcycle ID ({motorcycle.Id}) {motorcycle.Model} from year {motorcycle.Year} registered.",
                        CreatedAt = DateTime.UtcNow
                    };

                    await notificationService.AddNotificationAsync(notification);
                    _logger.LogInformation("Notification saved to the database.");
                }
            }
        };

        _channel.BasicConsume(queue: _rabbitMqService.QueueName,
                              autoAck: true,
                              consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
