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
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            //Set Pagination Params
            int resultLength = 10;
            int pageNumber = pageNum;


            //Declare var Books to make data manipulation simpler
            var Books = BookRepo.Books
                .Where(x => x.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * resultLength)
                .Take(resultLength);

            //Pass Data to backend
            var data = new BooksViewModel
            {
                //Books to view on this page
                Books = Books,

                PageInfo = new PageInfo
                {

                    TotalNumBooks = (
                        bookCategory == null 
                        ? BookRepo.Books.Count() 
                        : BookRepo.Books
                        .Where(x => x.Category == bookCategory)
                        .Count()),
                    BooksPerPage = resultLength,
                    CurrentPage = pageNum
                }
            };

            
            
            return View(data);
        }
    }
}