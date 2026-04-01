using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Loans;

public class ReturnBookCommand : ICommand
{
    private readonly ILoanService _loanService;
    private readonly IBookService _bookService;

    public string Name => "Return Book";

    public ReturnBookCommand(ILoanService loanService, IBookService bookService)
    {
        _loanService = loanService;
        _bookService = bookService;
    }

    public async Task ExecuteAsync()
    {
        var loanedBooks = _bookService.GetAll().Where(b => !b.IsAvailable).ToList();

        if (loanedBooks.Count == 0)
        {
            Console.WriteLine("No books currently on loan.");
            return;
        }

        Console.WriteLine($"\n{"ID",-38} {"Title",-30} Author");
        Console.WriteLine(new string('-', 85));
        foreach (var book in loanedBooks)
            Console.WriteLine($"{book.Id,-38} {book.Title,-30} {book.Author}");

        Console.Write("\nEnter the Book ID to return: ");
        var input = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(input, out var bookId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await _loanService.ReturnBook(bookId);
        Console.WriteLine("Book returned successfully.");
    }
}
