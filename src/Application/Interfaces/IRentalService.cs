using motcyApi.Domain.Entities;

namespace motcyApi.Application.Interfaces;

public interface IRentalService
{
    Task<Rental> CreateRentalAsync(Rental rental);
    Task<Rental> ReturnMotorcycleAsync(string rentalId, DateTime returnDate);
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<Rental> GetRentalByIdAsync(string id);
}
