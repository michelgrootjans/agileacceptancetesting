using System.Collections.Generic;
using Snacks_R_Us.WebApp.IoC;
using Snacks_R_Us.WebApp.Repositories;
using Snacks_R_Us.WebApp.Services;

namespace Snacks_R_Us.WebApp
{
    public static class ApplicationStartup
    {
        private static bool applicationHasBeenStarted;

        public static void Run()
        {
            if (applicationHasBeenStarted) 
                return;

            InitializeContainer();
            applicationHasBeenStarted = true;
        }

        private static void InitializeContainer()
        {
            var container = CreateContainer();
            Container.InitializeWith(container);
        }

        private static DictionaryContainer CreateContainer()
        {
            //This is where you can switch your IoC container of choice
            var services = new List<object>();

            IRepository repository = new InMemoryDatabase();
            services.Add(new OrderService(repository));

            return new DictionaryContainer(services);
        }
    }
}