using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class MotorcycleYearNotificationConsumer : BackgroundService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MotorcycleYearNotificationConsumer> _logger;
    private IConnection? _connection;
    private IModel? _channel;

    public MotorcycleYearNotificationConsumer(
        IRabbitMqService rabbitMqService,
        IServiceScopeFactory scopeFactory,
        ILogger<MotorcycleYearNotificationConsumer> logger
    )
    {
        _rabbitMqService = rabbitMqService;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    private void InitializeRabbitMQListener()
    {
        try
        {
            var factory = _rabbitMqService.GetConnectionFactory();
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMqService.GetQueueName(),
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _logger.LogInformation("RabbitMQ listener initialized");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error initializing RabbitMQ listener: {ex.Message}");
            throw;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    InitializeRabbitMQListener();
                }

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    var eventData = JsonConvert.DeserializeObject<dynamic>(jsonMessage);

                    if (eventData == null)
                    {
                        _logger.LogError("Error deserializing RabbitMQ message");
                        return;
                    }

                    if (eventData.Event == "MotorcycleYearNotification")
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                            var motorcycle = eventData.Data.ToObject<Motorcycle>();
                            _logger.LogInformation($"Processing notification for motorcycle: {motorcycle.Model}, Year: {motorcycle.Year}");

                            var notification = new Notification(
                                $"Motorcycle ID ({motorcycle.Id}) {motorcycle.Model} from year {motorcycle.Year} registered.",
                                DateTime.UtcNow
                            );

                            await notificationService.AddNotificationAsync(notification);
                            _logger.LogInformation("Notification saved to the database.");
                        }
                    }
                };

                _channel.BasicConsume(queue: _rabbitMqService.GetQueueName(),
                                      autoAck: true,
                                      consumer: consumer);

                await Task.Delay(1000, stoppingToken); // Poll every second to check for new messages
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing RabbitMQ messages: {ex.Message}");
                await Task.Delay(5000, stoppingToken); // Retry connection after 5 seconds if there's an error
            }
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
