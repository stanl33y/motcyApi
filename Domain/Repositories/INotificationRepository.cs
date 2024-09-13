public interface INotificationRepository
{
    Task<Notification> AddNotificationAsync(Notification notification);
}
