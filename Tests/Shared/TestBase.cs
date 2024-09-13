using Microsoft.EntityFrameworkCore;

public class TestBase
{
    protected readonly AppDbContext _context;

    public TestBase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SharedTestDatabase")
            .Options;

        _context = new AppDbContext(options);

        _context.Database.EnsureCreated();
    }
}
