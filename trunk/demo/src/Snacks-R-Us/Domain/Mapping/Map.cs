using System.Collections.Generic;
using Snacks_R_Us.Domain.IoC;

namespace Snacks_R_Us.Domain.Mapping
{
    public class Map
    {
        public static Mapper<From> This<From>(From from)
        {
            return new Mapper<From>(from);
        }

        public static ListMapper<From> These<From>(IEnumerable<From> from)
        {
            return new ListMapper<From>(from);
        }
    }

    public class Mapper<From>
    {
        private readonly From item;

        public Mapper(From item)
        {
            this.item = item;
        }

        public To ToA<To>()
        {
            var mapper = Container.GetImplementationOf<IMapper<From, To>>();
            return mapper.Map(item);
        }
    }

    public class ListMapper<From>
    {
        private readonly IEnumerable<From> source;

        public ListMapper(IEnumerable<From> from)
        {
            source = from;
        }

        public IEnumerable<To> ToAListOf<To>()
        {
            var mapper = Container.GetImplementationOf<IMapper<From, To>>();
            foreach (var item in source)
                yield return mapper.Map(item);
        }
    }
}