
using System.Threading.Tasks;
using LibraryBookTracker.Repositories;
using LibraryBookTracker.Services;

namespace LibraryBookTracker;

public class Program
{
    public static async Task Main(string[] agrs)
    {
        var bookFileStorage = new FileStorageService("books.json");
        var clientFileStorage = new FileStorageService("clients.json");
        var loanFileStorage = new FileStorageService("loans.json");

        var bookRepository = new BookRepository(bookFileStorage);
        var clientRepository = new ClientRepository(clientFileStorage);
        var loanRepository = new LoanRepository(loanFileStorage);

        var bookService = new BookService(bookRepository);
        var clientService = new ClientService(clientRepository);
        var loanService = new LoanService(bookRepository, loanRepository);

        await bookRepository.LoadFromFileAsync();
        await clientRepository.LoadFromFileAsync();
        await loanRepository.LoadFromFileAsync();

        Console.WriteLine("=== Library Book Tracker ===");

        bool running = true;

        while (running)
        {
            
        }
    }
}