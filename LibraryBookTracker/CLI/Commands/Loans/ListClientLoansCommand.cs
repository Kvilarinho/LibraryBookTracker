using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Loans;

public class ListClientLoansCommand : ICommand
{
    private readonly ILoanService _loanService;
    private readonly IClientService _clientService;

    public string Name => "List Loans by Client";

    public ListClientLoansCommand(ILoanService loanService, IClientService clientService)
    {
        _loanService = loanService;
        _clientService = clientService;
    }

    public Task ExecuteAsync()
    {
        var clients = _clientService.GetAll().ToList();

        if (clients.Count == 0)
        {
            Console.WriteLine("No clients registered.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\n{"ID",-38} {"First Name",-20} Last Name");
        Console.WriteLine(new string('-', 80));
        foreach (var client in clients)
            Console.WriteLine($"{client.Id,-38} {client.FirstName,-20} {client.LastName}");

        Console.Write("\nEnter the Client ID: ");
        var input = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(input, out var clientId))
        {
            Console.WriteLine("Invalid ID format.");
            return Task.CompletedTask;
        }

        var loans = _loanService.GetByClient(clientId).ToList();

        if (loans.Count == 0)
        {
            Console.WriteLine("No loans found for this client.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\n{"Book ID",-38} {"Loan Date",-22} Return Date");
        Console.WriteLine(new string('-', 85));
        foreach (var loan in loans)
        {
            var returnDate = loan.ReturnDate.HasValue
                ? loan.ReturnDate.Value.ToString("yyyy-MM-dd HH:mm")
                : "Not returned";
            Console.WriteLine($"{loan.BookId,-38} {loan.LoanDate,-22:yyyy-MM-dd HH:mm} {returnDate}");
        }

        Console.WriteLine();
        return Task.CompletedTask;
    }
}
