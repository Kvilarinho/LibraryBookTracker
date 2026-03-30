using System;

namespace LibraryBookTracker.Interfaces;

public interface ILoanService
{  
    void LoanBook(Guid bookId, Guid clientId);
    void ReturnBook(Guid bookId);

}
