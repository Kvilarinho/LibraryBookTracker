using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface ILoanService
{  
    Task LoanBook(Guid bookId, Guid clientId);
    Task ReturnBook(Guid bookId);
    IEnumerable<Loan> GetByClient(Guid clientId);
}
