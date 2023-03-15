using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Contollers
{
    public class OrderController : Controller
    {
        //Define Constructor for Repository Pattern and allow for Cart to be Passed in
        private IOrderRepository repo { get; set; }
        private Cart cart { get; set; }
        public OrderController(IOrderRepository temp, Cart c)
        {
            //Pass Repo and Cart into Object
            repo = temp;
            cart = c;
        }
        //View Form for Checkout ORdering
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        //Submit Order Form
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            //Ensure Checkout only happens when there are items to checkout
            if (cart.Items.Count() ==0)
            {
                ModelState.AddModelError("", "Sorry, your Cart is Empty!");
            }
            if (ModelState.IsValid)
            {
                //Add Items to Order, Pass to Repo and Clear Cart
                order.Items = cart.Items.ToArray();
                repo.SaveOrder(order);
                cart.ClearCart();

                return RedirectToPage("/OrderComplete");
            }
            else
            {
                return View();
            }
        }
    }
}
