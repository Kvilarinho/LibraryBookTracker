using LibraryBookTracker.Interfaces;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly ILoanRepository _loanRepository;

    public ClientService(IClientRepository repository, ILoanRepository loanRepository)
    {
        _repository = repository;
        _loanRepository = loanRepository;
    }

    public async Task AddClient(string firstName, string lastName, string phoneNumber)
    {
        var client = new Client(firstName, lastName, phoneNumber);
        _repository.Add(client);

        await _repository.SaveToFileAsync();
    }

    public IEnumerable<Client> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task RemoveClient(Guid id)
    {
        var hasActiveLoans = _loanRepository.GetAll()
            .Any(l => l.ClientId == id && l.ReturnDate == null);

        if (hasActiveLoans)
            throw new InvalidOperationException("Cannot remove a client with active loans.");

        _repository.Remove(id);
        await _repository.SaveToFileAsync();
    }
}
