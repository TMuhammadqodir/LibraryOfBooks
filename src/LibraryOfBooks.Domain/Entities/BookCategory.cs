﻿using LibraryOfBooks.Domain.Commons;

namespace LibraryOfBooks.Domain.Entities;

public class BookCategory : Auditable
{
    public string Name { get; set; }
}