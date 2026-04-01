using LibraryBookTracker.Interfaces;

namespace LibraryBookTracker.CLI.Commands.Clients;

public class AddClientCommand : ICommand
{
    private readonly IClientService _clientService;

    public string Name => "Add Client";

    public AddClientCommand(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("First name: ");
        var firstName = Console.ReadLine()?.Trim();

        Console.Write("Last name: ");
        var lastName = Console.ReadLine()?.Trim();

        Console.Write("Phone number: ");
        var phoneNumber = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phoneNumber))
        {
            Console.WriteLine("All fields are required.");
            return;
        }

        await _clientService.AddClient(firstName, lastName, phoneNumber);
        Console.WriteLine($"Client \"{firstName} {lastName}\" added successfully.");
    }
}
