namespace Snacks_R_Us.WebApp.Repositories
{
    public interface IRepository
    {
        long Save<T>(T t);
    }
}