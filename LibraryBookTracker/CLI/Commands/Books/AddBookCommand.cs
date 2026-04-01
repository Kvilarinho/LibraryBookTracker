using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Books;

public class AddBookCommand : ICommand
{
    private readonly IBookService _bookService;

    public string Name => "Add Book";

    public AddBookCommand(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine()?.Trim();

        Console.Write("Author: ");
        var author = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Title and author cannot be empty.");
            return;
        }

        await _bookService.AddBook(title, author);
        Console.WriteLine($"Book \"{title}\" added successfully.");
    }
}
