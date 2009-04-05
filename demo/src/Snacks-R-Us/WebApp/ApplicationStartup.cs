using System.Collections.Generic;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;

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

            var snackRepository = new SnackRepository();
            services.Add(new OrderService(new OrderRepository(), snackRepository));
            services.Add(new SnackService(snackRepository));
            services.Add(new SnackToDtoMapper());
            services.Add(new CreateOrderDtoMapper());
            services.Add(new OrderToDtoMapper());

            return new DictionaryContainer(services);
        }
    }
}