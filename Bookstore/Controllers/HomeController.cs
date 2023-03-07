using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.ViewModels;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        // creates a repository object
        private IBookstoreRepository repo;

        // constructs repository with the controller
        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }
        
        // takes category chossen and page number to return queried selection of books
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            // defines the amount of results on a page
            int pageSize = 10;

            // brings in a booksviewmodel that pulls the books from the database and orders them into pages
            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(p => p.Category == bookCategory || bookCategory == null)
                .OrderBy(p => p.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                // defines the pages and number of books on the page
                PageInfo = new PageInfo
                {
                    // total number of books depends on the category selected, or if there is one selected at all
                    TotalNumBooks = (bookCategory == null ? repo.Books.Count() : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
