public class DeliveryPersonService : IDeliveryPersonService
{
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;

    public DeliveryPersonService(IDeliveryPersonRepository deliveryPersonRepository)
    {
        _deliveryPersonRepository = deliveryPersonRepository;
    }

    public async Task<DeliveryPerson> RegisterDeliveryPersonAsync(DeliveryPerson deliveryPerson)
    {
        return await _deliveryPersonRepository.AddDeliveryPersonAsync(deliveryPerson);
    }

    public async Task<DeliveryPerson> GetDeliveryPersonByIdAsync(string id)
    {
        return await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id);
    }

    public async Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeopleAsync()
    {
        return await _deliveryPersonRepository.GetAllDeliveryPeopleAsync();
    }

    public async Task<DeliveryPerson> GetDeliveryPersonByCnpjAsync(string cnpj)
    {
        return await _deliveryPersonRepository.GetDeliveryPersonByCnpjAsync(cnpj);
    }

    public async Task<bool> UpdateLicenseImageAsync(string id, string image)
    {
        var deliveryPerson = await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id);
        if (deliveryPerson == null)
        {
            return false;
        }

        deliveryPerson.LicenseImage = image;
        await _deliveryPersonRepository.UpdateDeliveryPersonAsync(deliveryPerson);
        
        return true;
    }

    public async Task<bool> DeleteDeliveryPersonAsync(string id)
    {
        var deliveryPerson = await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id);
        if (deliveryPerson == null)
        {
            return false;
        }

        await _deliveryPersonRepository.DeleteDeliveryPersonAsync(id); 
        return true;
    }
}
