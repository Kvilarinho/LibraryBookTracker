using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Books;

public class RemoveBookCommand : ICommand
{
    private readonly IBookService _bookService;

    public string Name => "Remove Book";

    public RemoveBookCommand(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task ExecuteAsync()
    {
        var books = _bookService.GetAll().ToList();

        if (books.Count == 0)
        {
            Console.WriteLine("No books registered.");
            return;
        }

        Console.WriteLine($"\n{"ID",-38} {"Title",-30} Author");
        Console.WriteLine(new string('-', 80));
        
        foreach (var book in books)
            Console.WriteLine($"{book.Id,-38} {book.Title,-30} {book.Author}");

        Console.Write("\nEnter the Book ID to remove: ");
        var input = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(input, out var id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await _bookService.RemoveBook(id);
        Console.WriteLine("Book removed successfully.");
    }
}
