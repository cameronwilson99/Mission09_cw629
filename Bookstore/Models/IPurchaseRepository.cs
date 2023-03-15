using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface IPurchaseRepository
    {
        IQueryable<Purchase> Purchases { get; set; }

        void SavePurchase(Purchase p);
    }
}
