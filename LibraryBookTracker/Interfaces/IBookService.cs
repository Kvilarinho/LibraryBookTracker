using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IBookService
{
    void AddBook(string title, string author);
    void SetAvailable(Guid id, bool available);
    void RemoveBook(Guid id);
    IEnumerable<Book> GetAll();
    IEnumerable<Book> GetAvailable();
    

}
