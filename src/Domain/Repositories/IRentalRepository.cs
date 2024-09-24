using motcyApi.Domain.Entities;

namespace motcyApi.Domain.Repositories;

public interface IRentalRepository
{
    Task<Rental> AddRentalAsync(Rental rental);
    Task<Rental?> GetRentalByIdAsync(string id);
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<Rental> UpdateRentalAsync(Rental rental);
    Task<bool> DeleteRentalAsync(string id);
}
