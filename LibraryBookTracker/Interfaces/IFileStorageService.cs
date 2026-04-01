using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IFileStorageService
{
    Task SaveClientAsync(IEnumerable<Client> clients);
    Task SaveBookAsync(IEnumerable<Book> books);
    Task SaveLoanAsync(IEnumerable<Loan> loans);
    Task<List<Client>> LoadClientAsync();
    Task<List<Book>> LoadBookAsync();
    Task<List<Loan>> LoadLoanAsync();
}
