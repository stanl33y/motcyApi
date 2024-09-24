using Xunit;
using Moq;

public class DeliveryPersonServiceTests : TestBase
{
    private readonly DeliveryPersonService _deliveryPersonService;
    private readonly Mock<IDeliveryPersonRepository> _deliveryPersonRepositoryMock;
    private readonly Mock<IStorageService> _storageServiceMock;

    public DeliveryPersonServiceTests()
    {
        _deliveryPersonRepositoryMock = new Mock<IDeliveryPersonRepository>();
        _storageServiceMock = new Mock<IStorageService>();
        _deliveryPersonService = new DeliveryPersonService(
            _deliveryPersonRepositoryMock.Object,
            _storageServiceMock.Object
        );
    }

    [Fact]
    public async Task RegisterDeliveryPersonAsync_ShouldRegisterDeliveryPerson()
    {
        // Arrange
        var deliveryPerson = new DeliveryPerson(
            "del01",
            "John Doe",
            "12345678901234",
            new DateTime(1990, 1, 1),
            "12345678900",
            "A"
        );

        _deliveryPersonRepositoryMock.Setup(repo => repo.AddDeliveryPersonAsync(It.IsAny<DeliveryPerson>()))
            .ReturnsAsync(deliveryPerson);

        // Act
        var result = await _deliveryPersonService.RegisterDeliveryPersonAsync(deliveryPerson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        _deliveryPersonRepositoryMock.Verify(repo => repo.AddDeliveryPersonAsync(It.IsAny<DeliveryPerson>()), Times.Once);
    }

    [Fact]
    public async Task GetDeliveryPersonByIdAsync_ShouldReturnDeliveryPerson()
    {
        // Arrange
        var deliveryPerson = new DeliveryPerson(
            "del01",
            "Jane Doe",
            "12345678901234",
            new DateTime(1990, 1, 1),
            "12345678900",
            "A"
        );

        _deliveryPersonRepositoryMock.Setup(repo => repo.GetDeliveryPersonByIdAsync("del01"))
            .ReturnsAsync(deliveryPerson);

        // Act
        var result = await _deliveryPersonService.GetDeliveryPersonByIdAsync("del01");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane Doe", result.Name);
    }

    [Fact]
    public async Task GetAllDeliveryPeopleAsync_ShouldReturnAllDeliveryPeople()
    {
        // Arrange
        var deliveryPeople = new List<DeliveryPerson>
        {
            new DeliveryPerson(
                "del01",
                "John Doe",
                "12345678901234",
                new DateTime(1990, 1, 1),
                "12345678900",
                "A"
            ),

            new DeliveryPerson(
                "del02",
                "Jane Doe",
                "12345678901234",
                new DateTime(1990, 1, 1),
                "12345678900",
                "A"
            ),
        };

        _deliveryPersonRepositoryMock.Setup(repo => repo.GetAllDeliveryPeopleAsync())
            .ReturnsAsync(deliveryPeople);

        // Act
        var result = await _deliveryPersonService.GetAllDeliveryPeopleAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}
