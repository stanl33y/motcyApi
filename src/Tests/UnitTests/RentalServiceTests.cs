using Moq;
using Xunit;
using motcyApi.Tests.Shared;
using motcyApi.Application.Services;
using motcyApi.Domain.Repositories;
using motcyApi.Domain.Entities;

namespace motcyApi.Tests.UnitTests;

public class RentalServiceTests: TestBase
{
    private readonly RentalService _rentalService;
    private readonly Mock<IRentalRepository> _rentalRepositoryMock;
    private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
    private readonly Mock<IDeliveryPersonRepository> _deliveryPersonRepositoryMock;

    public RentalServiceTests()
    {
        _rentalRepositoryMock = new Mock<IRentalRepository>();
        _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
        _deliveryPersonRepositoryMock = new Mock<IDeliveryPersonRepository>();
        _rentalService = new RentalService(
            _rentalRepositoryMock.Object,
            _motorcycleRepositoryMock.Object,
            _deliveryPersonRepositoryMock.Object
        );
    }

    [Fact]
    public async Task CreateRentalAsync_ShouldCreateRental()
    {
        // Arrange
        var motorcycle = new Motorcycle ( "moto01", 2021, "Yamaha", "YMH-2021" );

        var deliveryPerson = new DeliveryPerson(
            "del01",
            "John Doe",
            "12345678901234",
            new DateTime(1990, 1, 1),
            "12345678900",
            "A"
        );

        _motorcycleRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync("moto01")).ReturnsAsync(motorcycle);
        _deliveryPersonRepositoryMock.Setup(repo => repo.GetDeliveryPersonByIdAsync("del01")).ReturnsAsync(deliveryPerson);

        var rental = new Rental(Guid.NewGuid().ToString(), motorcycle.Id, deliveryPerson.Id ?? "", DateTime.Now, DateTime.Now.AddDays(7), 7);
        _rentalRepositoryMock.Setup(repo => repo.AddRentalAsync(It.IsAny<Rental>())).ReturnsAsync(rental);

        // Act
        var result = await _rentalService.CreateRentalAsync(rental);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("moto01", result.MotorcycleId);
        _rentalRepositoryMock.Verify(repo => repo.AddRentalAsync(It.IsAny<Rental>()), Times.Once);
    }
}
