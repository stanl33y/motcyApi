public class DeliveryPersonService : IDeliveryPersonService
{
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;
    private readonly IStorageService _fileService;

    public DeliveryPersonService(IDeliveryPersonRepository deliveryPersonRepository, IStorageService fileService)
    {
        _deliveryPersonRepository = deliveryPersonRepository;
        _fileService = fileService;

    }

    public async Task<DeliveryPerson> RegisterDeliveryPersonAsync(DeliveryPerson deliveryPerson)
    {
        return await _deliveryPersonRepository.AddDeliveryPersonAsync(deliveryPerson);
    }

    public async Task<DeliveryPerson> GetDeliveryPersonByIdAsync(string id)
    {
        return await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id) ?? throw new InvalidOperationException("Delivery Person not found.");
    }

    public async Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeopleAsync()
    {
        return await _deliveryPersonRepository.GetAllDeliveryPeopleAsync();
    }

    public async Task<DeliveryPerson> GetDeliveryPersonByCnpjAsync(string cnpj)
    {
        return await _deliveryPersonRepository.GetDeliveryPersonByCnpjAsync(cnpj) ?? throw new InvalidOperationException("Delivery Person not found.");
    }

    public async Task<bool> UpdateLicenseImageAsync(string id, string image)
    {
        var deliveryPerson = await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id);
        if (deliveryPerson == null)
        {
            return false;
        }

        await _fileService.SaveFileFromBase64Async(image, $"{id}.png");

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
        await _fileService.DeleteFileAsync($"{id}.png");

        return true;
    }
}
