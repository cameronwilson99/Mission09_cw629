using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BooksViewModel
    {
        // creates an iqueryable object of type book, bringing in the book class from the scaffold
        public IQueryable<Book> Books { get; set; }
        // brings pageinfo viewmodel into this view model
        public PageInfo PageInfo { get; set; }
    }
}
