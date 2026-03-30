using System;

namespace LibraryBookTracker.Models;

public class Book : BaseEntity
{

    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public bool IsAvailable { get; set; } =  true;

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}
