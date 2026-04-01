using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Clients;

public class RemoveClientCommand : ICommand
{
    private readonly IClientService _clientService;

    public string Name => "Remove Client";

    public RemoveClientCommand(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task ExecuteAsync()
    {
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

        Console.Write("\nEnter the Client ID to remove: ");
        var input = Console.ReadLine()?.Trim();

        if (!Guid.TryParse(input, out var id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await _clientService.RemoveClient(id);
        Console.WriteLine("Client removed successfully.");
    }
}
