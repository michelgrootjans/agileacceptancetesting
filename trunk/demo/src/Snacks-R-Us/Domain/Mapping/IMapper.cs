using AutoMapper;

namespace Snacks_R_Us.Domain.Mapping
{
    public interface IMapper<From, To>
    {
        To Map(From from);
    }

    public class GenericAutoMapper<From, To> : IMapper<From, To>
    {
        public To Map(From from)
        {
            return Mapper.Map<From, To>(from);
        }
    }
}