using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Books;

public class ListBooksCommand : ICommand
{
    private readonly IBookService _bookService;

    public string Name => "List All Books";

    public ListBooksCommand(IBookService bookService)
    {
        _bookService = bookService;
    }

    public Task ExecuteAsync()
    {
        var books = _bookService.GetAll().ToList();

        if (books.Count == 0)
        {
            Console.WriteLine("No books registered.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\n{"ID",-38} {"Title",-30} {"Author",-25} Status");
        Console.WriteLine(new string('-', 100));

        foreach (var book in books)
        {
            var status = book.IsAvailable ? "Available" : "Loaned";
            Console.WriteLine($"{book.Id,-38} {book.Title,-30} {book.Author,-25} {status}");
        }

        Console.WriteLine();
        return Task.CompletedTask;
    }
}
