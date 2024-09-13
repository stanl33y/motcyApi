public interface IDeliveryPersonRepository
{
    Task<DeliveryPerson> AddDeliveryPersonAsync(DeliveryPerson deliveryPerson);      
    Task<DeliveryPerson?> GetDeliveryPersonByIdAsync(string id);
    Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeopleAsync();
    Task<DeliveryPerson?> GetDeliveryPersonByCnpjAsync(string cnpj);
    Task<DeliveryPerson?> UpdateDeliveryPersonAsync(DeliveryPerson deliveryPerson);
    Task<bool> DeleteDeliveryPersonAsync(string id);
}
