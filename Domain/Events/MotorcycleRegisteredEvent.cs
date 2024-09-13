public class MotorcycleRegisteredEvent
{
    public MotorcycleRegisteredEvent(string identificador, string modelo, int ano, string placa)
    {
        Identificador = identificador;
        Modelo = modelo;
        Ano = ano;
        Placa = placa;
        DataRegistro = DateTime.UtcNow;
    }

    public string Identificador { get; private set; }
    public string Modelo { get; private set; }
    public int Ano { get; private set; }
    public string Placa { get; private set; }
    public DateTime DataRegistro { get; private set; }
}
