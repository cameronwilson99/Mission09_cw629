using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;

namespace Bookstore.Models
{

    // initializes an iqueryable object of books, allowing for easier querying by the repository
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; }
    }
}
