using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using Snacks_R_Us.Domain.Extensions;

namespace Snacks_R_Us.Domain.Entities
{
    public class User : IPrincipal, IEntity
    {
        private static long idCounter;

        public long Id { get; private set; }
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
            this.roles = roles.IsNull() ? new List<string>() : new List<string>(roles);
            orders = new List<Order>();
            Credit = new Credit(0);
        }

        internal void ResetPasswordTo(string newPassword)
        {
            Password = newPassword;
        }

        public bool IsInRole(string role)
        {
            return roles.Exists(r => r.Equals(role, StringComparison.OrdinalIgnoreCase));
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
            Credit.Pay(order);
            AddToOrders(order);
        }

        public void AddCredits(double amount)
        {
            Credit.AddAmount(amount);
        }

        private void AddToOrders(Order order)
        {
            var orderForTheSameSnackToday =
                orders.Find(o => o.Date.Date.Equals(DateTime.Now.Date) && o.Snack.Name.Equals(order.Snack.Name));
            if (orderForTheSameSnackToday == null)
                orders.Add(order);
            else
                orderForTheSameSnackToday.Qty += order.Qty;
        }
    }
}