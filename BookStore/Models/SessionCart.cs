using BookStore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SessionCart : Cart
    {
        //Method to Get Basket
        public static Cart GetCart (IServiceProvider services)
        {
            //Create a Session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Create a Session Cart
            SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();

            //Instantiate Cart with Session
            cart.Session = session;

            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book bookIn, int qty)
        {
            //Extend Add Item, updates to JSON Session
            base.AddItem(bookIn, qty);
            Session.SetJSON("cart", this);
        }

        public override void RemoveItem(Book book)
        {
            //Extend Remove Item, update Session JSON
            base.RemoveItem(book);
            Session.SetJSON("cart", this);
        }

        public override void ClearCart()
        {
            //Extend Clear Cart, Update Session JSON
            base.ClearCart();
            Session.Remove("cart");
        }
    }
}
