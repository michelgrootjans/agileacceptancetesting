namespace Snacks_R_Us.Domain.Mapping
{
    public interface IMapper<From, To>
    {
        To Map(From from);
    }
}