using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class BuyModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public BuyModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public void OnGet(string returnUrl)
        {
            //takes url from the booksummary page and sets it as the return url. If there is none, it will set it to the home page
            ReturnUrl = returnUrl ?? "/";
            //returns cart found from the session, if there is none it will create a new one
            //cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //if cart exists, it will use the one from the session. If not, it will create a new one
            //cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            cart.AddItem(b, 1);

            //sets the current cart from the session to be updated and used across the site
            //HttpContext.Session.SetJson("cart", cart);

            //goes back to where the user clicked the buy button
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        //attaches to the html page where the remove button is located, handles the deletion of items from the cart
        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            //searches for item based on the id given in the form passed from the html file, sends it to the removeitem method of the cart class
            cart.RemoveItem(cart.Items.First(x => x.book.BookId == bookId).book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
