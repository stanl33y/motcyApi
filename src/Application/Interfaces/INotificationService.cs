using motcyApi.Domain.Entities;

namespace motcyApi.Application.Interfaces;

public interface INotificationService
{
    Task<Notification> AddNotificationAsync(Notification notification);
    Task NotifyMotorcycleRegisteredAsync(Motorcycle motorcycle);
    Task NotifyMotorcycleYearAsync(Motorcycle motorcycle);
}
