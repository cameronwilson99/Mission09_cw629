using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {

        //gets the database and attaches it to the context file, creates a repository framework
        private BookstoreContext context;
        public EFPurchaseRepository (BookstoreContext temp)
        {
            context = temp;
        }

        //gets the purchase record and includes the book record attached to it
        public IQueryable<Purchase> Purchases => context.Purchases.Include(x => x.Lines).ThenInclude(x => x.book);

        IQueryable<Purchase> IPurchaseRepository.Purchases { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SavePurchase(Purchase p)
        {
            //takes the purchase, adds the new record, and saves the changes to the context file
            context.AttachRange(p.Lines.Select(x => x.book));

            if (p.PurchaseId == 0)
            {
                context.Purchases.Add(p);
            }

            context.SaveChanges();
        }
    }
}
