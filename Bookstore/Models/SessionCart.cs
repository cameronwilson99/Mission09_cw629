using Bookstore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Models
{

    //inherits from the Cart class
    public class SessionCart : Cart
    {
        public static Cart GetCart (IServiceProvider services)
        {
            //gets info about the session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //if there is a session, it will go out and grab the cart from that session. Otherwise, it will create a new one
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            //updates the session information
            cart.Session = session;

            return cart;
        }

        //prevents an object from being serialized or deserialized
        [JsonIgnore]
        public ISession Session { get; set; }

        //these following functions override the functions of the Cart class and add Session functionality
        public override void AddItem(Book b, int qty)
        {
            base.AddItem(b, qty);
            Session.SetJson("Cart", this);
        }

        public override void RemoveItem(Book b)
        {
            base.RemoveItem(b);
            Session.SetJson("Cart", this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }
    }
}
