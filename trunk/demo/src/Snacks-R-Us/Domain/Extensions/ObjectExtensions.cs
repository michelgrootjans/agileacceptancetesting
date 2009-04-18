namespace Snacks_R_Us.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object target)
        {
            return target != null;
        }
    }
}