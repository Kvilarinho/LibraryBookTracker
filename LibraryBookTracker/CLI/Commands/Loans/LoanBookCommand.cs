using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Loans;

public class LoanBookCommand : ICommand
{
    private readonly ILoanService _loanService;
    private readonly IBookService _bookService;
    private readonly IClientService _clientService;

    public string Name => "Loan Book";

    public LoanBookCommand(ILoanService loanService, IBookService bookService, IClientService clientService)
    {
        _loanService = loanService;
        _bookService = bookService;
        _clientService = clientService;
    }

    public async Task ExecuteAsync()
    {
        var availableBooks = _bookService.GetAvailable().ToList();

        if (availableBooks.Count == 0)
        {
            Console.WriteLine("No books available for loan.");
            return;
        }

        Console.WriteLine($"\n{"ID",-38} {"Title",-30} Author");
        Console.WriteLine(new string('-', 85));
        foreach (var book in availableBooks)
            Console.WriteLine($"{book.Id,-38} {book.Title,-30} {book.Author}");

        Console.Write("\nEnter the Book ID to loan: ");
        var bookInput = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(bookInput, out var bookId))
        {
            Console.WriteLine("Invalid Book ID format.");
            return;
        }

        var clients = _clientService.GetAll().ToList();

        if (clients.Count == 0)
        {
            Console.WriteLine("No clients registered.");
            return;
        }

        Console.WriteLine($"\n{"ID",-38} {"First Name",-20} Last Name");
        Console.WriteLine(new string('-', 80));
        foreach (var client in clients)
            Console.WriteLine($"{client.Id,-38} {client.FirstName,-20} {client.LastName}");

        Console.Write("\nEnter the Client ID: ");
        var clientInput = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(clientInput, out var clientId))
        {
            Console.WriteLine("Invalid Client ID format.");
            return;
        }

        await _loanService.LoanBook(bookId, clientId);
        Console.WriteLine("Book loaned successfully.");
    }
}
