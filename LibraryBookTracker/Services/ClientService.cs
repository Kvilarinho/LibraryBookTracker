using System;
using System.Threading.Tasks;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task AddClient(string firstName, string lastName, string phoneNumber)
    {
        var client = new Client(firstName, lastName, phoneNumber);
        _repository.Add(client);

        await _repository.SaveToFileAsync();
    }

    public IEnumerable<Client> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task RemoveClient(Guid id)
    {
        _repository.Remove(id);
        await _repository.SaveToFileAsync();
    }
}
