using motcyApi.Application.Interfaces;
using motcyApi.Domain.Entities;
using motcyApi.Domain.Events;

namespace motcyApi.Infrastructure.Data.Messaging;

public class MotorcycleRegisteredEventHandler
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly AppDbContext _dbContext;
    private readonly INotificationService _notificationService;

    public MotorcycleRegisteredEventHandler(IRabbitMqService rabbitMqService, INotificationService notificationService, AppDbContext dbContext)
    {
        _rabbitMqService = rabbitMqService;
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task HandleAsync(MotorcycleRegisteredEvent motorcycleRegisteredEvent)
    {
        _rabbitMqService.SendMessage(motorcycleRegisteredEvent);

        var newMotorcycle = new Motorcycle(
            motorcycleRegisteredEvent.Id,
            motorcycleRegisteredEvent.Year,
            motorcycleRegisteredEvent.Model,
            motorcycleRegisteredEvent.Plate
        );

        await _notificationService.NotifyMotorcycleRegisteredAsync(newMotorcycle);
        await _notificationService.NotifyMotorcycleYearAsync(newMotorcycle);
    }
}
