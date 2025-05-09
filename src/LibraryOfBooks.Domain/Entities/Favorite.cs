﻿using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

public class Favorite : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long BookId { get; set; }
    public Book Book { get; set; }
}
