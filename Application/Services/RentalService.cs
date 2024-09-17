public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;

    public RentalService(
        IRentalRepository rentalRepository,
        IMotorcycleRepository motorcycleRepository,
        IDeliveryPersonRepository deliveryPersonRepository)
    {
        _rentalRepository = rentalRepository;
        _motorcycleRepository = motorcycleRepository;
        _deliveryPersonRepository = deliveryPersonRepository;
    }

    public async Task<Rental> ReturnMotorcycleAsync(int rentalId, DateTime returnDate)
    {
        var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
        if (rental == null)
        {
            throw new ArgumentException("Rental not found.");
        }

        rental.ReturnDate = returnDate;

        if (returnDate > rental.ExpectedEndDate)
        {
            rental.TotalCost += CalculateLateFees(rental.ExpectedEndDate, returnDate);
        }
        else if (returnDate < rental.ExpectedEndDate)
        {
            rental.TotalCost += CalculateEarlyReturnPenalty(rental, returnDate);
        }

        return await _rentalRepository.UpdateRentalAsync(rental);
    }

    private decimal CalculateEarlyReturnPenalty(Rental rental, DateTime returnDate)
    {
        int unutilizedDays = (rental.ExpectedEndDate - returnDate).Days;

        if (unutilizedDays <= 0)
        {
            return 0;
        }

        decimal penaltyRate = rental.RentalPlan switch
        {
            7 => 0.20M,
            15 => 0.40M,
            _ => 0.00M
        };

        decimal dailyRate = rental.RentalPlan switch
        {
            7 => 30.00M,
            15 => 28.00M,
            30 => 22.00M,
            45 => 20.00M,
            _ => 0.00M
        };

        decimal unutilizedCost = unutilizedDays * dailyRate;

        decimal penalty = unutilizedCost * penaltyRate;

        return unutilizedCost + penalty;
    }

    private decimal CalculateLateFees(DateTime expectedEndDate, DateTime returnDate)
    {
        int lateDays = (returnDate - expectedEndDate).Days;
        decimal lateFeePerDay = 50.00M;

        return lateDays * lateFeePerDay;
    }

    public async Task<Rental> CreateRentalAsync(
        string motorcycleId
        , string deliveryPersonId
        , int rentalPlan
        , DateTime startDate
        , DateTime? endDate
        , DateTime expectedEndDate
    )
    {
        var motorcycle = await _motorcycleRepository.GetMotorcycleByIdAsync(motorcycleId);
        if (motorcycle == null)
        {
            throw new ArgumentException("Motorcycle not found.");
        }

        var deliveryPerson = await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(deliveryPersonId);
        if (deliveryPerson == null)
        {
            throw new ArgumentException("Delivery person not found.");
        }

        decimal totalCost = CalculateRentalCost(rentalPlan, startDate, expectedEndDate);

        var rental = new Rental
        {
            MotorcycleId = motorcycle.Id,
            DeliveryPersonId = deliveryPerson.Id,
            StartDate = startDate,
            EndDate = endDate,
            ExpectedEndDate = expectedEndDate,
            TotalCost = totalCost,
            RentalPlan = rentalPlan
        };

        return await _rentalRepository.AddRentalAsync(rental);
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
    {
        return await _rentalRepository.GetAllRentalsAsync();
    }

    public async Task<Rental> GetRentalByIdAsync(int id)
    {
        return await _rentalRepository.GetRentalByIdAsync(id);
    }

    private decimal CalculateRentalCost(int rentalPlan, DateTime startDate, DateTime expectedEndDate)
    {
        decimal dailyRate = rentalPlan switch
        {
            7 => 30.00M,
            15 => 28.00M,
            30 => 22.00M,
            45 => 20.00M,
            _ => throw new ArgumentException("Invalid rental plan.")
        };

        var totalDays = (expectedEndDate - startDate).Days;
        return totalDays * dailyRate;
    }
}
