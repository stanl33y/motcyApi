public interface IRentalService
{
    Task<Rental> CreateRentalAsync(string motorcycleId, string deliveryPersonId, int rentalPlan, DateTime startDate, DateTime? endDate, DateTime expectedEndDate);
    Task<Rental> ReturnMotorcycleAsync(int rentalId, DateTime returnDate);
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<Rental> GetRentalByIdAsync(int id);
}
