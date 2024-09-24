namespace motcyApi.Domain.Events;

public class MotorcycleRegisteredEvent
{
    public MotorcycleRegisteredEvent(string id, string model, int year, string plate)
    {
        Id = id;
        Model = model;
        Year = year;
        Plate = plate;
        RegisterDate = DateTime.UtcNow;
    }

    public string Id { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string Plate { get; private set; }
    public DateTime RegisterDate { get; private set; }
}
