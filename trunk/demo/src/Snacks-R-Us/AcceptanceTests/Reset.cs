using System;
using fitlibrary;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Reset : DoFixture
    {
        private IRepository repository;

        public Reset()
        {
            Fitnesse.Init();
            repository = Container.GetImplementationOf<IRepository>();
        }

        public string Credits()
        {
            try
            {
                repository.FindAll<User>().ForEach(u => u.ClearCredits());
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string Orders()
        {
            try
            {
                repository.FindAll<User>().ForEach(u => u.ClearOrders());
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}