using Microsoft.EntityFrameworkCore;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;

    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Rental> AddRentalAsync(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        return rental;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
    {
        return await _context.Rentals.Include(r => r.Motorcycle).Include(r => r.DeliveryPerson).ToListAsync();
    }

    public async Task<Rental?> GetRentalByIdAsync(int id)
    {
        return await _context.Rentals.Include(r => r.Motorcycle).Include(r => r.DeliveryPerson).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Rental> UpdateRentalAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
        return rental;
    }

    public async Task<bool> DeleteRentalAsync(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null) return false;

        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();
        return true;
    }
}
