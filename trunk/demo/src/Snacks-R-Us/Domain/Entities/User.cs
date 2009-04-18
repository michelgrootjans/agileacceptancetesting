using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;

namespace Snacks_R_Us.Domain.Entities
{
    public class User : IPrincipal, IEntity
    {
        private static long idCounter;

        public long Id { get; private set;}
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        private readonly IEnumerable<string> roles;
        private readonly List<Order> orders;
        public Credit Credit { get; internal set; }

        public User(string name, string password, string email, params string[] roles)
        {
            Id = ++idCounter;
            Name = name;
            Password = password;
            Email = email;
            this.roles = new List<string>(roles);
            orders = new List<Order>();
            Credit = new Credit(0);
        }

        internal void ResetPasswordTo(string newPassword)
        {
            Password = newPassword;
        }

        public bool IsInRole(string role)
        {
            return roles.Contains(role);
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }

        public IEnumerable<Order> Orders
        {
            get { return new ReadOnlyCollection<Order>(orders); }
        }

        public void AddOrder(Order order)
        {
            if (order.TotalPrice > Credit.Amount)
                throw new InsufficientCreditsException(Credit.Amount, order.TotalPrice);

            var orderForTheSameSnackToday =
                orders.Find(o => o.Date.Date.Equals(DateTime.Now.Date) && o.SnackName.Equals(order.SnackName));
            if (orderForTheSameSnackToday == null)
                orders.Add(order);
            else
                orderForTheSameSnackToday.Qty += order.Qty;
            Credit.AddAmount(-order.TotalPrice);
        }

        public void AddCredits(double amount)
        {
            Credit.AddAmount(amount);
        }
    }
}