namespace Snacks_R_Us.WebApp.IoC
{
    public static class Container
    {
        private static IContainer container;

        public static void InitializeWith(IContainer currentContainer)
        {
            container = currentContainer;
        }

        public static T GetImplementationOf<T>()
        {
            return container.GetImplementationOf<T>();
        }
    }

    public interface IContainer
    {
        T GetImplementationOf<T>();
    }
}