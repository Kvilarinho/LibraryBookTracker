using System;
using System.Text.Json;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _bookFilePath;
    private readonly string _clientFilePath;

    public FileStorageService(string bookFilePath = "books.json", string clientFilePath = "clients.json")
    {
        _bookFilePath = bookFilePath;
        _clientFilePath = clientFilePath;
    }

    public async Task<List<Book>> LoadBookAsync()
    {
        if (!File.Exists(_bookFilePath)) return new List<Book>();

        var json = await File.ReadAllTextAsync(_bookFilePath);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new();
    }

    public async Task<List<Client>> LoadClientAsync()
    {
        if (!File.Exists(_clientFilePath)) return new List<Client>();

        var json = await File.ReadAllTextAsync(_clientFilePath);
        return JsonSerializer.Deserialize<List<Client>>(json) ?? new();
    }

    public async Task SaveBookAsync(IEnumerable<Book> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(_bookFilePath, json);
    }

    public async Task SaveClientAsync(IEnumerable<Client> clients)
    {
        var json = JsonSerializer.Serialize(clients, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(_clientFilePath, json);
    }
}
