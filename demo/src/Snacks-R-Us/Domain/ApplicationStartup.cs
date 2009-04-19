using System.Collections.Generic;
using System.Web;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.Domain.Extensions;

namespace Snacks_R_Us.Domain
{
    public static class ApplicationStartup
    {
        private static bool applicationHasBeenStarted;
        private static IRepository repository;

        public static void Run()
        {
            if (applicationHasBeenStarted) 
                return;

            InitializeRepository();
            InitializeContainer();
            applicationHasBeenStarted = true;
        }

        private static void InitializeRepository()
        {
            //You'll probably want to switch to NHibernate here
            repository = new OrderDecoratedRepository(new InMemoryRepository());
        }

        private static void InitializeContainer()
        {
            var container = CreateContainer();
            Container.InitializeWith(container);
        }

        private static IContainer CreateContainer()
        {
            //This is where you can switch your IoC container of choice
            //We've choosen poor man's dependency injection ;-)
            var services = new List<object>();

            services.Add(new AccountMembershipService(repository));

            //Dirty hack
            IAuthenticationService authenticationservice = new SimpleAuthenticationService();
            if (HttpContext.Current.IsNotNull())
                authenticationservice = new FormsAuthenticationService(authenticationservice);
            
            services.Add(authenticationservice);

            services.Add(new SnackService(repository));
            services.Add(new SnackToDtoMapper());
            services.Add(new CreateSnackDtoMapper());

            services.Add(new OrderService(repository));
            services.Add(new CreateOrderDtoMapper());
            services.Add(new OrderToDtoMapper());

            services.Add(new CreditService(repository));
            services.Add(new UserToCreditDtoMapper());

            services.Add(new UserService(repository));
            services.Add(new UserToDtoMapper());

            return new DictionaryContainer(services);
        }

        public static void AddDemoData()
        {
            InitUsers();
            InitSnacks();
        }

        private static void InitUsers()
        {
            var pascal = new User("pascal", "ihc", "pascal@ihc.be", "Secretary");
            var michel = new User("michel", "ilean", "michel@ilean.be", "Developer");
            michel.AddCredits(20);

            repository.Save(pascal);
            repository.Save(michel);
        }

        private static void InitSnacks()
        {
            repository.Save(new Snack("Pizza Hawaii", 5.5));
            repository.Save(new Snack("Club Sandwich", 3.5));
            repository.Save(new Snack("Ceasar's Salad", 4.2));
            repository.Save(new Snack("Tiramisu", 4.5));
            repository.Save(new Snack("Big Mac", 5.7));
        }
    }
}