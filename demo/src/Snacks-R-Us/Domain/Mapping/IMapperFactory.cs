using System;

namespace Snacks_R_Us.Domain.Mapping
{
    public interface IMapperFactory
    {
        IMapper<From, To> GetMapper<From, To>();
    }

   public class GenericMapperFactory : IMapperFactory
    {
        public IMapper<From, To> GetMapper<From, To>()
        {
            return new GenericAutoMapper<From, To>();
        }
    }
}