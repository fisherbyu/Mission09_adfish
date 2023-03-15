using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    //Extend Template
    public class EFOrderRepository : IOrderRepository
    {
        //Context object
        private BookstoreContext context;
        //constructor
        public EFOrderRepository(BookstoreContext temp)
        {
            //Set Context to Recieve
            context = temp;
        }

        //Fill Orders with Book Items from Cart
        public IQueryable<Order> Orders => context.Orders.Include(x => x.Items).ThenInclude(x => x.Book);

        //Submit Order
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Items.Select(x => x.Book));

            if (order.OrderId == 0)
            {
                //Save Order
                context.Orders.Add(order);
            }

            //Push to DB
            context.SaveChanges();
        }
    }
}
