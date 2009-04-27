using System.Collections.Generic;
using AutoMapper;

namespace Snacks_R_Us.Domain.Mapping
{
    public static class Map
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
            return Mapper.Map<From, To>(item);
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
            foreach (var item in source)
                yield return Mapper.Map<From, To>(item);
        }
    }
}