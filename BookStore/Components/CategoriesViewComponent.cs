using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Components
{
    public class CategoriesViewComponent : ViewComponent  
    {
        private IBookStoreRepository repo { get; set; }

        public CategoriesViewComponent(IBookStoreRepository repoIn)
        {
            repo = repoIn;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CurrCat = RouteData?.Values["bookCategory"];
            var types = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return View(types);
        }
    }
}
