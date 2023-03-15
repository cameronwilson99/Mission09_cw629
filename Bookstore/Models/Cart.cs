using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Cart
    {
        public List<ItemInCart> Items { get; set; } = new List<ItemInCart>();

        public virtual void AddItem(Book b, int qty)
        {
            ItemInCart Line = Items
                .Where(x => x.book.BookId == b.BookId)
                .FirstOrDefault();

            if (Line == null)
            {
                Items.Add(new ItemInCart
                {
                    book = b,
                    Quantity = qty
                });
            }
            else
            {
                Line.Quantity += qty;
            }
        }

        // looks into the items and finds the item that matches the id given
        public virtual void RemoveItem(Book b)
        {
            Items.RemoveAll(x => x.book.BookId == b.BookId);
        }

        // clears the list of items in the cart
        public virtual void ClearCart()
        {
            Items.Clear();
        }

        public double CalcTotal()
        {
            double total = Items.Sum(x => x.Quantity * x.book.Price);
            return total;
        }
    }

    public class ItemInCart
    {
        [Key]
        public int LineID { get; set; }
        public Book book { get; set; }
        public int Quantity { get; set; }
    }
}
