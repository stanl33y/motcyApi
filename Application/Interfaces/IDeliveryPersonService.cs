public interface IDeliveryPersonService
{
    Task<DeliveryPerson> RegisterDeliveryPersonAsync(DeliveryPerson deliveryPerson);
    Task<DeliveryPerson> GetDeliveryPersonByIdAsync(string id);
    Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeopleAsync();
    Task<bool> UpdateLicenseImageAsync(string id, string imagePath);
    Task<DeliveryPerson> GetDeliveryPersonByCnpjAsync(string cnpj);
    Task<bool> DeleteDeliveryPersonAsync(string id);
}
