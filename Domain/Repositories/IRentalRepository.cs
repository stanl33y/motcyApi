public interface IRentalRepository
{
    Task<Rental> AddRentalAsync(Rental rental);
    Task<Rental?> GetRentalByIdAsync(int id);
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<Rental> UpdateRentalAsync(Rental rental);
    Task<bool> DeleteRentalAsync(int id);
}
