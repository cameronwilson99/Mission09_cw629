using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        //bring in a repository
        private IBookstoreRepository repo { get; set; }

        //constructor brings in the repository when called
        public CategoryViewComponent(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            //gets category from the url route, passing it to the default page
            ViewBag.SelectedCategory = RouteData.Values["bookCategory"];
            //builds database for the component, takes data from the repository, only selecting distince categories. Passing to view
            var categories = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
