using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;

namespace Bookstore.Models
{
    // creates an entity framework of the bookstore repository, bringing in the context of the database. Also brings in a iqueryable of books,
    // representing a collection of books from the database
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }

        public EFBookstoreRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Book> Books => context.Books;
    }
}
