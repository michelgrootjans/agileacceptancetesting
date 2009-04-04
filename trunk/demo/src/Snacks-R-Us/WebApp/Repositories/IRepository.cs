namespace Snacks_R_Us.WebApp.Repositories
{
    public interface IRepository
    {
        void Save<T>(T t);
    }
}