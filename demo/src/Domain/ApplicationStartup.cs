using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.Domain
{
    public static class ApplicationStartup
    {
        public static void Run()
        {
            var repository = InitializeRepository();
            InitializeContainer(repository);
            InitializeMappers();
        }

        private static IRepository InitializeRepository()
        {
            //You'll probably want to switch to NHibernate here
            return new OrderDecoratedRepository(new InMemoryRepository());
        }

        private static void InitializeContainer(IRepository repository)
        {
            var container = CreateContainer(repository);
            Container.InitializeWith(container);
        }

        private static IContainer CreateContainer(IRepository repository)
        {
            //This is where you can switch your IoC container of choice
            //We've choosen poor man's dependency injection ;-)
            var services = new List<object>();

            services.Add(repository);
            services.Add(new AccountMembershipService(repository));

            //Dirty hack
            IAuthenticationService authenticationservice = new SimpleAuthenticationService();
            if (HttpContext.Current.IsNotNull())
                authenticationservice = new FormsAuthenticationService(authenticationservice);

            services.Add(authenticationservice);

            services.Add(new GenericMapperFactory());
            services.Add(new SnackService(repository));
            services.Add(new OrderService(repository));
            services.Add(new CreditService(repository));
            services.Add(new UserService(repository));

            return new DictionaryContainer(services);
        }

        public static void InitializeMappers()
        {
            Mapper.CreateMap<User, ViewUserDto>();
            Mapper.CreateMap<Order, ViewOrderDto>();
            
            Mapper.CreateMap<IEnumerable<Order>, ViewOrdersDto>()
                .ForMember(dto => dto.Total, conf => conf.MapFrom(orders => orders.Sum(o => o.TotalPrice)))
                .ForMember(dto => dto.Orders, conf => conf.MapFrom(orders => orders));
            
            Mapper.CreateMap<User, ViewCreditDto>()
                .ForMember(dto => dto.UserId, conf => conf.MapFrom(u => u.Id))
                .ForMember(dto => dto.UserName, conf => conf.MapFrom(u => u.Name));
            
            Mapper.CreateMap<Snack, SnackDto>()
                .ForMember(dto => dto.ScreenName,
                           conf => conf.MapFrom(s => string.Format("{0} (� {1})", s.Name, s.Price)));

            Mapper.CreateMap<CreateSnackDto, Snack>()
                .ConvertUsing(dto => new Snack(dto.Name, double.Parse(dto.Price)));
            
            Mapper.CreateMap<CreateOrderDto, Order>()
                .ConvertUsing(dto => new Order { Qty = double.Parse(dto.Qty) });

            Mapper.AssertConfigurationIsValid();
        }
    }
}