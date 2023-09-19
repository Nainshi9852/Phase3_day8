using System;
using System.Collections.Generic;

namespace BookManagement.Models;

public partial class BookStore
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string PublisherName { get; set; } = null!;

    public double? Price { get; set; }

    public string? Category { get; set; }
}
