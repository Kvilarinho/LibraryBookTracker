using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface ILoanService
{  
    void LoanBook(Guid bookId, Guid clientId);
    void ReturnBook(Guid bookId);
    IEnumerable<Book> GetByClient(Guid clientId);
}
