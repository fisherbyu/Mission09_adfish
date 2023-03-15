using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public interface IOrderRepository 
    {
        //Create Template for Order Repo:
        //Orders
        public IQueryable<Order> Orders { get; }

        //Save Method
        public void SaveOrder(Order o)
        {

        }
    }
}
