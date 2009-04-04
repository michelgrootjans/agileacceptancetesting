namespace Snacks_R_Us.WebApp
{
    public interface IMapper<From, To>
    {
        To Map(From from);
    }
}