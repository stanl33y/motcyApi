using Moq;
using Xunit;
using System.Threading.Tasks;

public class DeliveryPersonServiceTests: TestBase
{
    private readonly DeliveryPersonService _deliveryPersonService;
    private readonly Mock<IDeliveryPersonRepository> _deliveryPersonRepositoryMock;

    public DeliveryPersonServiceTests()
    {
        _deliveryPersonRepositoryMock = new Mock<IDeliveryPersonRepository>();
        _deliveryPersonService = new DeliveryPersonService(_deliveryPersonRepositoryMock.Object);
    }

    [Fact]
    public async Task RegisterDeliveryPersonAsync_ShouldRegisterDeliveryPerson()
    {
        // Arrange
        var deliveryPerson = new DeliveryPerson
        {
            Id = "del01",
            Name = "John Doe",
            Cnpj = "12345678901234",
            LicenseNumber = "12345678900",
            LicenseType = "A",
            LicenseImage = "base64string"
        };
        
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
        var deliveryPerson = new DeliveryPerson
        {
            Id = "del01",
            Name = "Jane Doe"
        };

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
            new DeliveryPerson { Id = "del01", Name = "John Doe" },
            new DeliveryPerson { Id = "del02", Name = "Jane Doe" }
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
