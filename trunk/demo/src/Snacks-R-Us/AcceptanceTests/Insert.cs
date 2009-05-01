using fitlibrary;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Insert : DoFixture
    {
        private readonly IRepository repository;
        private const string OK = "OK";

        public Insert()
        {
            Fitnesse.Reset();
            repository = Container.GetImplementationOf<IRepository>();
        }

        public string SampleData()
        {
            SampleUsers();
            SampleSnacks();
            return OK;
        }
        
        public string SampleUsers()
        {
            ApplicationStartup.AddDemoUsers(repository);
            return OK;
        }

        public string SampleSnacks()
        {
            ApplicationStartup.AddDemoSnacks(repository);
            return OK;
        }
    }
}