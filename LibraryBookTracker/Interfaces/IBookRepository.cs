using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IBookRepository
{
    void Add(Book book);
    void Remove(Guid id);
    IEnumerable<Book> GetAll();
}
