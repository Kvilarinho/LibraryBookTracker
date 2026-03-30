using System;
using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Repositories;

public class BookRepository : IBookRepository
{

    private readonly List<Book> _books = new();
    private readonly IFileStorageService _fileStorageService;

    public BookRepository(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task LoadFromFileAsync()
    {
        var loaded = await _fileStorageService.LoadBookAsync();
        _books.AddRange(loaded);
    }

    public async Task SaveToFileAsync()
    {
        await _fileStorageService.SaveBookAsync(_books);
    }


    public void Add(Book book)
    {
        _books.Add(book);
    }

    public IEnumerable<Book> GetAll()
    {
        return _books;
    }

    public void Remove(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id)
            ?? throw new KeyNotFoundException($"Book {id} not found");
        _books.Remove(book);
    }
}
