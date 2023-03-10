using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore.Models;
using BookStore.Infrastructure;

namespace BookStore.Pages
{
    public class PurchaseModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }
        public string ReturnUrl { get; set; }
        public PurchaseModel (IBookStoreRepository temp)
        {
            //Pass Repo to object
            repo = temp;
        }
        public Cart cart { get; set; }

        //Reactions to Get REquest
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        //Reactions to Post Request
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            //Initialize new book to add
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //initialize cart object
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            //Add to cart
            cart.AddItem(b, 1);

            HttpContext.Session.SetJSON("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl});
        }
    }
}
