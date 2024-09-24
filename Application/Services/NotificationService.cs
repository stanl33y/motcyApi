using motcyApi.Application.Interfaces;
using motcyApi.Domain.Entities;
using motcyApi.Domain.Repositories;

namespace motcyApi.Application.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IRabbitMqService _rabbitMqService;
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(ILogger<NotificationService> logger
        , IRabbitMqService rabbitMqService
        , INotificationRepository notificationRepository)
    {
        _logger = logger;
        _rabbitMqService = rabbitMqService;
        _notificationRepository = notificationRepository;
    }

    public async Task<Notification> AddNotificationAsync(Notification notification)
    {
        return await _notificationRepository.AddNotificationAsync(notification);
    }

    public Task NotifyMotorcycleRegisteredAsync(Motorcycle motorcycle)
    {
        var message = $"Motorcycle registered: {motorcycle.Model}, {motorcycle.Year}, Plate: {motorcycle.Plate}";

        _logger.LogInformation($"Sending notification: {message}");

        _rabbitMqService.SendMessage(new { Event = "MotorcycleRegistered", Data = motorcycle });

        return Task.CompletedTask;
    }

    public Task NotifyMotorcycleYearAsync(Motorcycle motorcycle)
    {
        if (motorcycle.Year == 2024)
        {
            var message = $"Motorcycle {motorcycle.Model} is from the year 2024.";

            _logger.LogInformation($"Sending notification for 2024 motorcycle: {message}");

            _rabbitMqService.SendMessage(new { Event = "MotorcycleYearNotification", Data = motorcycle });
        }

        return Task.CompletedTask;
    }

}
