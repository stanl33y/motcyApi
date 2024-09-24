using Microsoft.EntityFrameworkCore;
using motcyApi.Domain.Entities;
using motcyApi.Domain.Repositories;

namespace motcyApi.Infrastructure.Data.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly AppDbContext _context;

    public MotorcycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Motorcycle> AddMotorcycleAsync(Motorcycle motorcycle)
    {
        _context.Motorcycles.Add(motorcycle);
        await _context.SaveChangesAsync();
        return motorcycle;
    }

    public async Task<Motorcycle?> GetMotorcycleByIdAsync(string id)
    {
        return await _context.Motorcycles.FindAsync(id);
    }

    public async Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
    {
        return await _context.Motorcycles.ToListAsync();
    }

    public async Task<Motorcycle?> GetMotorcycleByPlateAsync(string plate)
    {
        return await _context.Motorcycles.FirstOrDefaultAsync(m => m.Plate == plate);
    }

    public async Task<Motorcycle?> UpdateMotorcycleAsync(Motorcycle motorcycle)
    {
        var existingMotorcycle = await _context.Motorcycles.FindAsync(motorcycle.Id);

        if (existingMotorcycle == null)
        {
            return null;
        }

        existingMotorcycle.Plate = motorcycle.Plate;
        existingMotorcycle.Model = motorcycle.Model;
        existingMotorcycle.Year = motorcycle.Year;

        await _context.SaveChangesAsync();
        return existingMotorcycle;
    }

    public async Task<bool> DeleteMotorcycleAsync(string id)
    {
        var motorcycle = await _context.Motorcycles.FindAsync(id);
        if (motorcycle == null)
        {
            return false;
        }

        _context.Motorcycles.Remove(motorcycle);
        await _context.SaveChangesAsync();
        return true;
    }
}
