using motcyApi.Domain.Entities;

namespace motcyApi.Domain.Repositories;

public interface INotificationRepository
{
    Task<Notification> AddNotificationAsync(Notification notification);
}
