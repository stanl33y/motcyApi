using motcyApi.Domain.Entities;

namespace motcyApi.Domain.Repositories;

public interface IMotorcycleRepository
{
    Task<Motorcycle> AddMotorcycleAsync(Motorcycle motorcycle);
    Task<Motorcycle?> GetMotorcycleByIdAsync(string id);
    Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
    Task<Motorcycle?> GetMotorcycleByPlateAsync(string plate);
    Task<Motorcycle?> UpdateMotorcycleAsync(Motorcycle motorcycle);
    Task<bool> DeleteMotorcycleAsync(string id);
}
