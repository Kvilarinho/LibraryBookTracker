namespace LibraryBookTracker.CLI;

public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync();
}
