namespace LibraryBookTracker.CLI.Menus;

public class MainMenu
{
    private readonly Dictionary<string, ICommand> _commands;

    public MainMenu(IEnumerable<ICommand> commands)
    {
        // Numera os comandos automaticamente: "1", "2", ...
        _commands = commands
            .Select((cmd, index) => (Key: (index + 1).ToString(), Command: cmd))
            .ToDictionary(x => x.Key, x => x.Command);
    }

    public async Task RunAsync()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Main Menu ===");
            foreach (var (key, command) in _commands)
                Console.WriteLine($"  {key}. {command.Name}");
            Console.WriteLine("  0. Exit");
            Console.Write("\nChoose an option: ");

            var input = Console.ReadLine()?.Trim();

            if (input == "0")
            {
                running = false;
                continue;
            }

            if (_commands.TryGetValue(input ?? string.Empty, out var selected))
            {
                Console.WriteLine();
                try
                {
                    await selected.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        Console.WriteLine("Goodbye!");
    }
}
