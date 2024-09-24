using motcyApi.Domain.Entities;

namespace motcyApi.Application.Interfaces;

public interface IMotorcycleService
{
    Task<Motorcycle> AddMotorcycleAsync(Motorcycle motorcycle);
    Task<Motorcycle> GetMotorcycleByIdAsync(string id);
    Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
    Task<Motorcycle> UpdateMotorcyclePlateAsync(string id, string newPlate);
    Task<bool> DeleteMotorcycleAsync(string id);
    Task<Motorcycle> GetMotorcycleByPlateAsync(string plate);
}
