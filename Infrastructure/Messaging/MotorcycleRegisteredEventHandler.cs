public class MotorcycleRegisteredEventHandler
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly AppDbContext _dbContext;
    private readonly INotificationService _notificationService;

    public MotorcycleRegisteredEventHandler(IRabbitMqService rabbitMqService, AppDbContext dbContext, INotificationService notificationService)
    {
        _rabbitMqService = rabbitMqService;
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(MotorcycleRegisteredEvent motorcycleRegisteredEvent)
    {
        _rabbitMqService.SendMessage(motorcycleRegisteredEvent);

        var newMotorcycle = new Motorcycle
        {
            Id = motorcycleRegisteredEvent.Identificador,
            Model = motorcycleRegisteredEvent.Modelo,
            Year = motorcycleRegisteredEvent.Ano,
            Plate = motorcycleRegisteredEvent.Placa
        };

        await _notificationService.NotifyMotorcycleRegisteredAsync(newMotorcycle);
        await _notificationService.NotifyMotorcycleYearAsync(newMotorcycle);
    }
}
