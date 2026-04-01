
using LibraryBookTracker.CLI;
using LibraryBookTracker.CLI.Commands.Books;
using LibraryBookTracker.CLI.Commands.Clients;
using LibraryBookTracker.CLI.Commands.Loans;
using LibraryBookTracker.CLI.Menus;
using LibraryBookTracker.Repositories;
using LibraryBookTracker.Services;

namespace LibraryBookTracker;

public class Program
{
    public static async Task Main(string[] args)
    {
        var bookFileStorage = new FileStorageService("books.json");
        var clientFileStorage = new FileStorageService("clients.json");
        var loanFileStorage = new FileStorageService("loans.json");

        var bookRepository = new BookRepository(bookFileStorage);
        var clientRepository = new ClientRepository(clientFileStorage);
        var loanRepository = new LoanRepository(loanFileStorage);

        var bookService = new BookService(bookRepository);
        var clientService = new ClientService(clientRepository, loanRepository);
        var loanService = new LoanService(bookRepository, loanRepository);

        await bookRepository.LoadFromFileAsync();
        await clientRepository.LoadFromFileAsync();
        await loanRepository.LoadFromFileAsync();

        var commands = new List<ICommand>
        {
            new AddBookCommand(bookService),
            new ListBooksCommand(bookService),
            new RemoveBookCommand(bookService),
            new AddClientCommand(clientService),
            new ListClientsCommand(clientService),
            new RemoveClientCommand(clientService),
            new LoanBookCommand(loanService, bookService, clientService),
            new ReturnBookCommand(loanService, bookService),
            new ListClientLoansCommand(loanService, clientService),
        };

        Console.WriteLine("=== Library Book Tracker ===");

        var menu = new MainMenu(commands);
        await menu.RunAsync();
    }
}