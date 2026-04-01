using System.Text.Json.Serialization;

namespace LibraryBookTracker.Models;

public class Loan : BaseEntity
{
    public Guid BookId { get; init; }
    public Guid? ClientId { get; set; }
    public DateTime LoanDate { get; init; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Guid bookId, Guid clientId)
    {
        BookId = bookId;
        ClientId = clientId;
        LoanDate = DateTime.Now;
    }

    [JsonConstructor]
    private Loan() { }
}