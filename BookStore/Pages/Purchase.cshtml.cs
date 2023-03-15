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

        public Cart cart { get; set; }
        public PurchaseModel (IBookStoreRepository temp, Cart c)
        {
            //Pass Repo to object
            repo = temp;

            //Create Cart
            cart = c;

        }
        

        //Reactions to Get REquest
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        //Reactions to Post Request
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            //Initialize new book to add
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //Add to cart
            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl});
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            //Remove Item from Session Cart
            cart.RemoveItem(
                cart.Items.First(x => x.Book.BookId == bookId).Book
                );

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
