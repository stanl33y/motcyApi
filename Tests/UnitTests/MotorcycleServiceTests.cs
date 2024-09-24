using Moq;
using Xunit;

public class MotorcycleServiceTests : TestBase
{
    private readonly MotorcycleService _motorcycleService;
    private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly MotorcycleRegisteredEventHandler _motorcycleRegisteredEventHandler;

    public MotorcycleServiceTests()
    {
        _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _notificationServiceMock = new Mock<INotificationService>();

        _motorcycleRegisteredEventHandler = new MotorcycleRegisteredEventHandler(
            _rabbitMqServiceMock.Object,
            _notificationServiceMock.Object,
            _context
        );

        _motorcycleService = new MotorcycleService(
            _motorcycleRepositoryMock.Object,
            _motorcycleRegisteredEventHandler
        );
    }

    [Fact]
    public async Task AddMotorcycleAsync_ShouldAddMotorcycle()
    {
    // public Motorcycle(string id, int year, string model, string plate)
        // Arrange
        var motorcycle = new Motorcycle( "moto01", 2020,  "Honda", "HND-2020" );
        _motorcycleRepositoryMock.Setup(repo => repo.AddMotorcycleAsync(It.IsAny<Motorcycle>()))
            .ReturnsAsync(motorcycle);

        // Configura o mock para o RabbitMqService
        _rabbitMqServiceMock.Setup(rmq => rmq.SendMessage(It.IsAny<MotorcycleRegisteredEvent>())).Verifiable();
        _notificationServiceMock.Setup(ns => ns.NotifyMotorcycleRegisteredAsync(It.IsAny<Motorcycle>())).Returns(Task.CompletedTask).Verifiable();
        _notificationServiceMock.Setup(ns => ns.NotifyMotorcycleYearAsync(It.IsAny<Motorcycle>())).Returns(Task.CompletedTask).Verifiable();

        // Act
        var result = await _motorcycleService.AddMotorcycleAsync(motorcycle);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Honda", result.Model);
        _motorcycleRepositoryMock.Verify(repo => repo.AddMotorcycleAsync(It.IsAny<Motorcycle>()), Times.Once);
        _rabbitMqServiceMock.Verify(rmq => rmq.SendMessage(It.IsAny<MotorcycleRegisteredEvent>()), Times.Once);
        _notificationServiceMock.Verify(ns => ns.NotifyMotorcycleRegisteredAsync(It.IsAny<Motorcycle>()), Times.Once);
        _notificationServiceMock.Verify(ns => ns.NotifyMotorcycleYearAsync(It.IsAny<Motorcycle>()), Times.Once);
    }
}
