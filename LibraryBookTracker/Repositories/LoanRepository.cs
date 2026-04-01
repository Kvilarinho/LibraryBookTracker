using System;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Repositories;

public class LoanRepository : ILoanRepository
{

    private readonly List<Loan> _loans = new();

    private readonly IFileStorageService _fileStorageService;


    public LoanRepository(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task LoadFromFileAsync()
    {
        var loaded = await _fileStorageService.LoadLoanAsync();
        _loans.AddRange(loaded);
    }

    public async Task SaveToFileAsync()
    {
        await _fileStorageService.SaveLoanAsync(_loans);
    }
    public void Add(Loan loan)
    {
        _loans.Add(loan);
    }

    public IEnumerable<Loan> GetAll()
    {
        return _loans; 
    }

    public void Remove(Guid id)
    {
        var loan = _loans.FirstOrDefault(l => l.Id == id) 
            ?? throw new KeyNotFoundException($"Loan {id} not found");
        _loans.Remove(loan);
    }
}
