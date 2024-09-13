public class MotorcycleService : IMotorcycleService
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly MotorcycleRegisteredEventHandler _motorcycleRegisteredEventHandler;

    public MotorcycleService(
        IMotorcycleRepository motorcycleRepository
        , MotorcycleRegisteredEventHandler motorcycleRegisteredEventHandler
    ) {
        _motorcycleRepository = motorcycleRepository;
        _motorcycleRegisteredEventHandler = motorcycleRegisteredEventHandler;
    }

    public async Task<Motorcycle> AddMotorcycleAsync(Motorcycle motorcycle)
    { 
        var newMotorcycle = await _motorcycleRepository.AddMotorcycleAsync(motorcycle);  

        var motorcycleEvent = new MotorcycleRegisteredEvent(newMotorcycle.Id, newMotorcycle.Model, newMotorcycle.Year, newMotorcycle.Plate);
        await _motorcycleRegisteredEventHandler.HandleAsync(motorcycleEvent);

        return newMotorcycle;
    }

    public async Task<Motorcycle> GetMotorcycleByIdAsync(string id)
    {
        return await _motorcycleRepository.GetMotorcycleByIdAsync(id);
    }

    public async Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
    {
        return await _motorcycleRepository.GetAllMotorcyclesAsync();
    }

    public async Task<Motorcycle> UpdateMotorcyclePlateAsync(string id, string newPlate)
    {
        var motorcycle = await _motorcycleRepository.GetMotorcycleByIdAsync(id);
        if (motorcycle == null)
        {
            return null;
        }

        motorcycle.Plate = newPlate;
        return await _motorcycleRepository.UpdateMotorcycleAsync(motorcycle);
    }

    public async Task<Motorcycle> GetMotorcycleByPlateAsync(string plate)
    {
        return await _motorcycleRepository.GetMotorcycleByPlateAsync(plate);
    }

    public async Task<bool> DeleteMotorcycleAsync(string id)
    {
        return await _motorcycleRepository.DeleteMotorcycleAsync(id);
    }
}
