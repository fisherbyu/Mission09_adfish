using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart
    {
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        public void AddItem(Book bookIn, int qty)
        {
            //Create new Book Line Item
            CartLineItem line = Items
                .Where(x => x.Book.BookId == bookIn.BookId)
                .FirstOrDefault();

            //if not in cart, create new line
            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = bookIn,
                    Quantity = qty
                });
            }

            //Or else, increment quantity
            else
            {
                line.Quantity += qty;
            }
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 25);

            return sum;
        }
    }

    public class CartLineItem
    {
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
