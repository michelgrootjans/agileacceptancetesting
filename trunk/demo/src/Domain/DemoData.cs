using System;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain
{
    public static class DemoData
    {
        public static void AddDeveloper(string userName, string password, string email, double credit)
        {
            AddUser(userName, password, email, credit, "Developer");
        }

        private static void AddSecretary(string userName, string password, string email, double credit)
        {
            AddUser(userName, password, email, credit, "Secretary");
        }

        public static void AddSnack(string snackName, double price)
        {
            Repository.Save(new Snack(snackName, price));
        }

        public static void AddDemoUsers()
        {
            AddSecretary("pascal", "ihc", "pascal@ihc.be", 0);
            AddDeveloper("michel", "ilean", "michel@ilean.be", 15);
            AddDeveloper("W. Fall", "bigdesignupfront", "wouter@BDUF.com", 1000);
        }

        public static void AddDemoSnacks()
        {
            AddSnack("Pizza Hawaii", 5.3);
            AddSnack("Club Sandwich", 3.5);
            AddSnack("Ceasar's Salad", 4.2);
            AddSnack("Tiramisu", 4.5);
            AddSnack("Big Mac", 5.7);
        }

        private static void AddUser(string userName, string password, string email, double credit, string role)
        {
            var user = new User(userName, password, email, role);
            Repository.Save(user);
            user.AddCredits(credit);
        }

        private static IRepository Repository
        {
            get { return Container.GetImplementationOf<IRepository>(); }
        }
    }
}