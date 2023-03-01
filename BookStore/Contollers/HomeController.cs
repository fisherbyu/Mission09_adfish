using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.ViewModels;

namespace BookStore.Contollers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository BookRepo;

        public HomeController(IBookStoreRepository temp) => BookRepo = temp;
        public IActionResult Index(int pageNum = 1)
        {
            int resultLength = 10;
            int pageNumber = pageNum;

            var data = new BooksViewModel
            {
                Books = BookRepo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * resultLength)
                .Take(resultLength),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = BookRepo.Books.Count(),
                    BooksPerPage = resultLength,
                    CurrentPage = pageNum
                }
            };

            
            
            return View(data);
        }
    }
}