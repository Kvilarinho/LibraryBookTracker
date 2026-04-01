using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IBookService
{
    Task AddBook(string title, string author);
    void SetAvailable(Guid id, bool available);
    Task RemoveBook(Guid id);
    IEnumerable<Book> GetAll();
    IEnumerable<Book> GetAvailable();

}
