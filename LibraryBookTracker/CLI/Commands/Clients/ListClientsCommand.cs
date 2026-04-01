using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Clients;

public class ListClientsCommand : ICommand
{
    private readonly IClientService _clientService;

    public string Name => "List All Clients";

    public ListClientsCommand(IClientService clientService)
    {
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

        Console.WriteLine($"\n{"ID",-38} {"First Name",-20} {"Last Name",-20} Phone");
        Console.WriteLine(new string('-', 95));

        foreach (var client in clients)
            Console.WriteLine($"{client.Id,-38} {client.FirstName,-20} {client.LastName,-20} {client.PhoneNumber}");

        Console.WriteLine();
        return Task.CompletedTask;
    }
}
