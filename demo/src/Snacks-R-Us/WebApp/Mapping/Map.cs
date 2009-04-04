using Snacks_R_Us.WebApp.IoC;

namespace Snacks_R_Us.WebApp
{
    public class Map
    {
        public static Mapper<From> This<From>(From from)
        {
            return new Mapper<From>(from);
        }
    }

    public class Mapper<From>
    {
        private readonly From from;

        public Mapper(From from)
        {
            this.from = from;
        }

        public To ToA<To>()
        {
            var mapper = Container.GetImplementationOf<IMapper<From, To>>();
            return mapper.Map(from);
        }
    }
}