using Microsoft.EntityFrameworkCore;
using motcyApi.Domain.Entities;
using motcyApi.Domain.Repositories;

namespace motcyApi.Infrastructure.Data.Repositories;

public class DeliveryPersonRepository : IDeliveryPersonRepository
{
    private readonly AppDbContext _context;

    public DeliveryPersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeliveryPerson> AddDeliveryPersonAsync(DeliveryPerson deliveryPerson)
    {
        _context.DeliveryPeople.Add(deliveryPerson);
        await _context.SaveChangesAsync();
        return deliveryPerson;
    }

    public async Task<DeliveryPerson?> GetDeliveryPersonByIdAsync(string id)
    {
        return await _context.DeliveryPeople.FindAsync(id);
    }

    public async Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeopleAsync()
    {
        return await _context.DeliveryPeople.ToListAsync();
    }

    public async Task<DeliveryPerson?> GetDeliveryPersonByCnpjAsync(string cnpj)
    {
        return await _context.DeliveryPeople.FirstOrDefaultAsync(d => d.Cnpj == cnpj);
    }

    public async Task<DeliveryPerson?> UpdateDeliveryPersonAsync(DeliveryPerson deliveryPerson)
    {
        var existingDeliveryPerson = await _context.DeliveryPeople.FindAsync(deliveryPerson.Id);

        if (existingDeliveryPerson == null)
        {
            return null;
        }

        existingDeliveryPerson.Name = deliveryPerson.Name;
        existingDeliveryPerson.Cnpj = deliveryPerson.Cnpj;
        existingDeliveryPerson.DateOfBirth = deliveryPerson.DateOfBirth;
        existingDeliveryPerson.LicenseNumber = deliveryPerson.LicenseNumber;
        existingDeliveryPerson.LicenseType = deliveryPerson.LicenseType;

        await _context.SaveChangesAsync();
        return existingDeliveryPerson;
    }

    public async Task<bool> DeleteDeliveryPersonAsync(string id)
    {
        var deliveryPerson = await _context.DeliveryPeople.FindAsync(id);
        if (deliveryPerson == null)
        {
            return false;
        }

        _context.DeliveryPeople.Remove(deliveryPerson);
        await _context.SaveChangesAsync();
        return true;
    }
}
