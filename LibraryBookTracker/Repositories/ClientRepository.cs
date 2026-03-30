using System;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients = new();
    private readonly IFileStorageService _fileStorageService;

    public ClientRepository(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task LoadFromFileAsync()
    {
        var loaded = await _fileStorageService.LoadClientAsync();
        _clients.AddRange(loaded);
    }

    public async Task SaveToFileAsync()
    {   
        await _fileStorageService.SaveClientAsync(_clients);
    }

    public void Add(Client client)
    {
        _clients.Add(client);
    }

    public void Remove(Guid id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id)
            ?? throw new KeyNotFoundException($"Client {id} not found");
        _clients.Remove(client);
    }

    public IEnumerable<Client> GetAll()
    {
        return _clients;
    }
}
