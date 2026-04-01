using System;
using System.Threading.Tasks;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task AddBook(string title, string author)
    {
        Book book = new Book(title, author);
        _repository.Add(book);

        await _repository.SaveToFileAsync();
    }

    public IEnumerable<Book> GetAll()
    {
        return _repository.GetAll();
    }

    public IEnumerable<Book> GetAvailable()
    {
        return _repository.GetAll().Where(b => b.IsAvailable);
    }

    public async Task RemoveBook(Guid id)
    {
        _repository.Remove(id);
        await _repository.SaveToFileAsync();
    }

    public void SetAvailable(Guid id, bool available)
    {
        Book book = _repository.GetAll().FirstOrDefault(b => b.Id == id)
            ?? throw new KeyNotFoundException($"Book {id} not found");

        book.IsAvailable = available;
        
    }
}
