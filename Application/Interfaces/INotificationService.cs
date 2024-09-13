public interface INotificationService
{
    Task<Notification> AddNotificationAsync(Notification notification);
    Task NotifyMotorcycleRegisteredAsync(Motorcycle motorcycle);
    Task NotifyMotorcycleYearAsync(Motorcycle motorcycle);
}
