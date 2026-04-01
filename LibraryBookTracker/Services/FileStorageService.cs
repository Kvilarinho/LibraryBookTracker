using System;
using System.Text.Json;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _bookFilePath;
    private readonly string _clientFilePath;
    private readonly string _loanFilePath;

    public FileStorageService(string bookFilePath = "books.json", string clientFilePath = "clients.json", string loanFilePath = "loan.json")
    {
        _bookFilePath = bookFilePath;
        _clientFilePath = clientFilePath;
        _loanFilePath = loanFilePath;
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

    public async Task<List<Loan>> LoadLoanAsync()
    {
        if (!File.Exists(_loanFilePath)) return new List<Loan>();

        var json = await File.ReadAllTextAsync(_loanFilePath);
        return JsonSerializer.Deserialize<List<Loan>>(json) ?? new();
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

    public async Task SaveLoanAsync(IEnumerable<Loan> loans)
    {
        var json = JsonSerializer.Serialize(loans, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_loanFilePath, json);
    }
}
