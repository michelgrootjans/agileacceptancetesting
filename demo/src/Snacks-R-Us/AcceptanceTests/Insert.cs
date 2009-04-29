using System;
using fitlibrary;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Insert : DoFixture
    {
        private readonly IRepository repository;

        public Insert()
        {
            Fitnesse.Init();
            repository = Container.GetImplementationOf<IRepository>();
        }

        public string SampleUsers()
        {
            try
            {
                ApplicationStartup.AddDemoUsers(repository);
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string SampleSnacks()
        {
            try
            {
                ApplicationStartup.AddDemoSnacks(repository);
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}