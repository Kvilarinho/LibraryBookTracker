using System;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class LoanService : ILoanService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;


    public LoanService(IBookRepository bookRepository, ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
    }

    public IEnumerable<Loan> GetByClient(Guid clientId)
    {
        return _loanRepository.GetAll().Where(b => b.ClientId == clientId);
    }

    public async Task LoanBook(Guid bookId, Guid clientId)
    {
        var book = _bookRepository.GetAll().FirstOrDefault(b => b.Id == bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found");

        if (!book.IsAvailable)
            throw new InvalidOperationException($"Book {bookId} is not available");

        var loan = new Loan(bookId, clientId);
        book.IsAvailable = false;
        _loanRepository.Add(loan);

        await _loanRepository.SaveToFileAsync();
        await _bookRepository.SaveToFileAsync();

    }

    public async Task ReturnBook(Guid bookId)
    {
        var book = _bookRepository.GetAll().FirstOrDefault(b => b.Id == bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found");

        var loan = _loanRepository.GetAll().FirstOrDefault(b => b.BookId == bookId)
            ?? throw new KeyNotFoundException($"Loan {bookId} not found");

        loan.ReturnDate = DateTime.Now;
        //_loanRepository.Remove(loan.Id); nao faz sentido, não guarda histórico
        book.IsAvailable = true;

        await _loanRepository.SaveToFileAsync();
        await _bookRepository.SaveToFileAsync();
    }
}
