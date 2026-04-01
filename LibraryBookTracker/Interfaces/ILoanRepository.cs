using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface ILoanRepository
{
    void Add(Loan loan);
    void Remove(Guid id);
    IEnumerable<Loan> GetAll();
}
